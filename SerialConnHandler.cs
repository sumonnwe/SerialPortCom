using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.CompilerServices;
using NETSFUNCTION.DataResponseModel;
using NETSFUNCTION.Helper;
using NLog;
using System.Timers;
using System.Xml.Linq;
using System.Collections;
using System.Text;
using System.Reflection.PortableExecutable;
using Azure.Core;
using static System.TimeZoneInfo;
using System.Security.Cryptography;
using NLog.Fluent;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Drawing;

namespace NETSFUNCTION
{
    public class SerialConnHandler
    {
        static bool _continue = true;
        public static SerialPort _serialPort;
        //static byte[] buffer = new byte[50];
        //private bool stop = false;

        private readonly string port;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //private byte[] command;
        byte[] ackCode = new byte[1];
        private static System.Timers.Timer aTimer;

        private delegate void UpdateUiTextDelegate(string text);

        public SerialConnHandler(string CommPort)
        {
            port = CommPort; 
             
            try
            {
                Console.WriteLine("============= Serial Conn Start ============");
                _serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
                // Set the read/write timeouts
                _serialPort.ReadTimeout = 5000;
                _serialPort.WriteTimeout = 10000;
                _serialPort.DtrEnable = true;
                _serialPort.RtsEnable = true;
                _serialPort.Handshake = Handshake.None;
                _serialPort.Open(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine("============= Serial Conn Error ============");
                Console.WriteLine(ex.Message);
            }
        }

        public void Start(Type modelName, byte[] commandBytes)
        {
            string name;
            string message;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;


            if (_serialPort != null && _serialPort.IsOpen)
            { 
                _continue = true;
                _serialPort.Write(commandBytes, 0, commandBytes.Length);
            } 
             
        }

        public void Read(Type modelName)
        { 
            while (_continue)
            {
                try
                {
                    Thread.Sleep(1000);
                    SerialDataReceivedEventHandler myEvent = (sender, e) => PortDataReceived(modelName);
                    _serialPort.DataReceived += myEvent; 
                }
                catch (TimeoutException ex)
                {
                    _continue = false;
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (!_continue)
            { 
                _serialPort.DiscardInBuffer();
                Thread.Sleep(2000);

            }
        }

        public void ProcessCreditSettlementData(List<byte> responseData, Type modelName)
        {
            CommonHelper c = new CommonHelper();
            ResponseMessageHeader responseHeader = new ResponseMessageHeader();

            string[] messageData = new string[] { };

            messageData = BitConverter.ToString(responseData.Take(responseData.Count - 1).ToArray()).Replace("-", "").Split(new string[] { "1C" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messageData.Length; i++)
            {
                if (messageData[i] != null)
                {
                    string msg = messageData[i] + "1C";

                    byte[] dataMessage = FromHex(msg);
                    string s = Encoding.ASCII.GetString(dataMessage); // GatewayServer


                    Console.WriteLine($"{i} ... " + $"{msg}" + " , " + $"{s}");
                }
            }


            //Response Body Data Setup 
            if (modelName != null)
            {
                  

                string[] fieldNames = modelName.GetProperties().Select(p => p.Name).ToArray();

                //Credit Settlement
                string[] chfieldNames = typeof(CreditHostResponse).GetProperties().Select(p => p.Name).ToArray();
                string[] ippfieldNames = typeof(IPPHostResponse).GetProperties().Select(p => p.Name).ToArray();
                string[] dccfieldNames = typeof(DCCHostResponse).GetProperties().Select(p => p.Name).ToArray();
                if (modelName.GetProperty("CreditHost").PropertyType == typeof(List<CreditHostResponse>))
                {
                    fieldNames = fieldNames.Concat(chfieldNames).ToArray();
                }
                if (modelName.GetProperty("IPPHost").PropertyType == typeof(List<IPPHostResponse>))
                {
                    fieldNames = fieldNames.Concat(ippfieldNames).ToArray();
                }
                if (modelName.GetProperty("DCCHost").PropertyType == typeof(List<DCCHostResponse>))
                {
                    fieldNames = fieldNames.Concat(dccfieldNames).ToArray();
                }
                fieldNames = fieldNames.Distinct().ToArray();
                var modelTest = new List<KeyValuePair<string, string>>();

                Dictionary<string, string> fieldCodeNamePair = c.PairFieldCode(fieldNames);

                //Header Data Setup
                int start = 0;
                string messageString = Encoding.ASCII.GetString(FromHex(messageData[0]));
                string[] headerField = typeof(ResponseMessageHeader).GetProperties().Select(p => p.Name).ToArray();
                foreach (string header in headerField)
                {
                    string len = (string)typeof(LenHelper).GetField(header).GetValue(typeof(LenHelper));
                    string msg = messageString.Substring(start, int.Parse(len));
                    start += int.Parse(len);
                    responseHeader.GetType().GetProperty(header).SetValue(responseHeader, msg);

                    Console.WriteLine("HeaderFieldName = " + header + " , HeaderFieldLength = " + len + ", HeaderFieldData = " + msg); //typeof(FieldCodeHelper).GetField(fieldNames[index])
                }

                for (int index = 0; index < messageData.Length; index++)
                {

                    if (messageData[index].Trim() != null)
                    {
                        //Console.WriteLine("Message = " + messageList[index].Trim());
                        string messageFieldCode = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(0, 4)));
                        string space = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(4, 2)));
                        string len = messageData[index].Substring(6, 2);
                        string data = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(8)));
                        string fieldName = fieldCodeNamePair.Where(p => p.Value == messageFieldCode).Select(p => p.Key).FirstOrDefault();

                        // string prevFieldCode = Encoding.ASCII.GetString(FromHex(messageData[index-1].Substring(0, 4)));


                        if (fieldName != null)
                        {
                            Console.WriteLine("Message = " + messageData[index].Trim() + "///// FieldName = " + fieldName + ", i = " + index + ", FieldCode = " + messageFieldCode + ", Len = " + len + ", Data = " + data); //typeof(FieldCodeHelper).GetField(fieldNames[index])  

                            modelTest.Add(new KeyValuePair<string, string>(fieldName, data)); 

                        }
                    }
                }

                CreditSettlementResponseModel credit = new CreditSettlementResponseModel();
                int i = 0;
                foreach (KeyValuePair<string, string> kvp in modelTest)
                {
                    if (kvp.Key != "CreditHost" && kvp.Key != "IPPHost" && kvp.Key != "DCCHost" && !IsSubListKey(kvp.Key, chfieldNames) && !IsSubListKey(kvp.Key, ippfieldNames) && !IsSubListKey(kvp.Key, dccfieldNames))
                    {
                        Console.WriteLine("key = " + kvp.Key + ", " + kvp.Value);
                        credit.GetType().GetProperty(kvp.Key).SetValue(credit, kvp.Value);
                    }

                    if (kvp.Key == "NumOfCreditHost" && int.Parse(kvp.Value) > 0)
                    {
                        List<CreditHostResponse> crd = GetCreditHostResponse("NumOfCreditHost", i, int.Parse(kvp.Value), modelTest);
                        credit.CreditHost = crd;
                    }

                    if (kvp.Key == "NumofDCCHost" && int.Parse(kvp.Value) > 0)
                    {
                        List<DCCHostResponse> dcc = GetDCCHostResponse("NumofDCCHost", i, int.Parse(kvp.Value), modelTest);
                        credit.DCCHost = dcc;
                    }

                    if (kvp.Key == "NumofIPPHost" && int.Parse(kvp.Value) > 0)
                    {
                        List<IPPHostResponse> ipp = GetIPPHostResponse("NumofIPPHost", i, int.Parse(kvp.Value), modelTest);
                        credit.IPPHost = ipp;
                    }
                    i++;
                }

                string output = JsonSerializer.Serialize(credit);

                Console.WriteLine(" JSON FORMAT : " + output);
            }
        }

        public void ProcessSettlementData(List<byte> responseData, Type modelName)
        {
            CommonHelper c = new CommonHelper();
            ResponseMessageHeader responseHeader = new ResponseMessageHeader();

            string[] messageData = new string[] { };

            messageData = BitConverter.ToString(responseData.Take(responseData.Count - 1).ToArray()).Replace("-", "").Split(new string[] { "1C" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messageData.Length; i++)
            {
                if (messageData[i] != null)
                {
                    string msg = messageData[i] + "1C";

                    byte[] dataMessage = FromHex(msg);
                    string s = Encoding.ASCII.GetString(dataMessage); // GatewayServer
                   
                    
                    Console.WriteLine($"{i} ... " + $"{msg}" + " , " + $"{s}"); 
                }
            }

            
            //Response Body Data Setup 
            if (modelName != null)
            {
                //DBModel.NetsPurchase netsKeep = new DBModel.NetsPurchase();
                //CreditSettlementResponseModel credit = new CreditSettlementResponseModel();

                var modelTest = new List<KeyValuePair<string, string>>();
                string[] fieldNames = modelName.GetProperties().Select(p => p.Name).ToArray();

                Dictionary<string, string> fieldCodeNamePair = c.PairFieldCode(fieldNames);

                //Header Data Setup
                int start = 0;
                string messageString = Encoding.ASCII.GetString(FromHex(messageData[0]));
                string[] headerField = typeof(ResponseMessageHeader).GetProperties().Select(p => p.Name).ToArray();
                foreach (string header in headerField)
                {
                    string len = (string)typeof(LenHelper).GetField(header).GetValue(typeof(LenHelper));
                    string msg = messageString.Substring(start, int.Parse(len));
                    start += int.Parse(len);
                    responseHeader.GetType().GetProperty(header).SetValue(responseHeader, msg);

                    Console.WriteLine("HeaderFieldName = " + header + " , HeaderFieldLength = " + len + ", HeaderFieldData = " + msg); //typeof(FieldCodeHelper).GetField(fieldNames[index])
                }

                for (int index = 0; index < messageData.Length; index++)
                {                     
                    if (messageData[index].Trim() != null)
                    {
                        //Console.WriteLine("Message = " + messageList[index].Trim());
                        string messageFieldCode = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(0, 4)));
                        string space = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(4, 2)));
                        string len = messageData[index].Substring(6, 2);
                        string data = messageData[index].Substring(8); //Encoding.ASCII.GetString(c.FromHex(messageData[index].Substring(8)));
                        string fieldName = fieldCodeNamePair.Where(p => p.Value == messageFieldCode).Select(p => p.Key).FirstOrDefault(); 
                       

                        if (fieldName != null)
                        {
                            Console.WriteLine("Message = " + messageData[index].Trim() + "///// FieldName = " + fieldName + ", i = " + index + ", FieldCode = " + messageFieldCode + ", Len = " + len + ", Data = " + data); //typeof(FieldCodeHelper).GetField(fieldNames[index])  
                            modelTest.Add(new KeyValuePair<string, string>(fieldName, data));
                          
                        }
                    } 
                }

                SettlementResponseModel settlement = new SettlementResponseModel();
                int i = 0; 
                foreach (KeyValuePair<string, string> kvp in modelTest)
                {
                    if (settlement.GetType().GetProperty(kvp.Key).PropertyType == typeof(decimal))
                    {
                        Console.WriteLine("decimal = "+kvp.Key);
                        settlement.GetType().GetProperty(kvp.Key).SetValue(settlement, Decimal.Parse(kvp.Value));
                    }
                    else
                    {
                        Console.WriteLine("string ASCII = " + kvp.Key);
                        settlement.GetType().GetProperty(kvp.Key).SetValue(settlement, Encoding.ASCII.GetString(FromHex(kvp.Value)));  
                    } 
                    i++;
                }

                string output = JsonSerializer.Serialize(settlement);

                Console.WriteLine(" JSON FORMAT : " + output);

            } 
        }

        public void ProcessData(List<byte> responseData, Type modelName)
        {
            CommonHelper c = new CommonHelper();
            ResponseMessageHeader responseHeader = new ResponseMessageHeader();

            string[] messageData = new string[] { };

            messageData = BitConverter.ToString(responseData.Take(responseData.Count - 1).ToArray()).Replace("-", "").Split(new string[] { "1C" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messageData.Length; i++)
            {
                if (messageData[i] != null)
                {
                    string msg = messageData[i] + "1C";

                    byte[] dataMessage = FromHex(msg);
                    string s = Encoding.ASCII.GetString(dataMessage); // GatewayServer


                    Console.WriteLine($"{i} ... " + $"{msg}" + " , " + $"{s}");
                }
            }


            //Response Body Data Setup 
            if (modelName != null)
            {
               
                string[] fieldNames = modelName.GetProperties().Select(p => p.Name).ToArray();

                Dictionary<string, string> fieldCodeNamePair = c.PairFieldCode(fieldNames);

                //Header Data Setup
                int start = 0;
                string messageString = Encoding.ASCII.GetString(FromHex(messageData[0]));
                string[] headerField = typeof(ResponseMessageHeader).GetProperties().Select(p => p.Name).ToArray();
                foreach (string header in headerField)
                {
                    string len = (string)typeof(LenHelper).GetField(header).GetValue(typeof(LenHelper));
                    string msg = messageString.Substring(start, int.Parse(len));
                    start += int.Parse(len);
                    responseHeader.GetType().GetProperty(header).SetValue(responseHeader, msg);

                    Console.WriteLine("HeaderFieldName = " + header + " , HeaderFieldLength = " + len + ", HeaderFieldData = " + msg); //typeof(FieldCodeHelper).GetField(fieldNames[index])
                }

                for (int index = 0; index < messageData.Length; index++)
                {

                    if (messageData[index].Trim() != null)
                    {
                        //Console.WriteLine("Message = " + messageList[index].Trim());
                        string messageFieldCode = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(0, 4)));
                        string space = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(4, 2)));
                        string len = messageData[index].Substring(6, 2);
                        string data = Encoding.ASCII.GetString(FromHex(messageData[index].Substring(8)));
                        string fieldName = fieldCodeNamePair.Where(p => p.Value == messageFieldCode).Select(p => p.Key).FirstOrDefault();


                        if (fieldName != null)
                        {
                            Console.WriteLine("Message = " + messageData[index].Trim() + "///// FieldName = " + fieldName + ", i = " + index + ", FieldCode = " + messageFieldCode + ", Len = " + len + ", Data = " + data); //typeof(FieldCodeHelper).GetField(fieldNames[index])  
 
                        }
                    }
                }
            }
        }


        private bool IsSubListKey(string key, string[] subField)
        {
            bool result = subField.Contains(key);
            return result;
        }

        private List<CreditHostResponse> GetCreditHostResponse(string key, int index, int count, List<KeyValuePair<string, string>> model)
        {            
            List<CreditHostResponse> list = new List<CreditHostResponse>();
            
            int startIndex = index + 1;
             
            for (int num = 0; num < count; num ++) 
            {
                CreditHostResponse chr = new CreditHostResponse();

                foreach (var prop in chr.GetType().GetProperties())
                {
                    Console.WriteLine(prop.Name + " , " + model.ElementAt(startIndex).Key + " , " + model.ElementAt(startIndex).Value);
                    if (prop.Name == model.ElementAt(startIndex).Key)
                    {
                        chr.GetType().GetProperty(prop.Name).SetValue(chr, model.ElementAt(startIndex).Value);
                    }
                    startIndex++;
                }
                Console.WriteLine(" list Add working ! " + chr.HostID );
                list.Add(chr);
            }      
            return list;
        }

        private List<IPPHostResponse> GetIPPHostResponse(string key, int index, int count, List<KeyValuePair<string, string>> model)
        {
            IPPHostResponse ipp = new IPPHostResponse();
            List<IPPHostResponse> list = new List<IPPHostResponse>();

            return list;
        }

        private List<DCCHostResponse> GetDCCHostResponse(string key, int index, int count, List<KeyValuePair<string, string>> model)
        {
            DCCHostResponse dcc = new DCCHostResponse();
            List<DCCHostResponse> list = new List<DCCHostResponse>();

            return list;
        }

        private void PortDataReceived( Type modelName)
        {
            Thread.Sleep(3000); 

            CommonHelper c = new CommonHelper();

            int bytes = _serialPort.BytesToRead;             
            byte[] buffer = new byte[bytes];
             
            _serialPort.Read(buffer, 0, buffer.Length);

            Console.WriteLine("Buffer Length " + buffer.Length);

            List<byte> data = new List<byte>();
            byte[] len = new byte[2];
            byte stx = new byte();
            byte etx = new byte();
            byte[] checksum = new byte[1];
            List<byte> responseData = new List<byte>();


            logger.Debug("BIT BUFFER = "+ BitConverter.ToString(buffer).Replace("-", " "));

            
            if (buffer.Length > 0)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (buffer[0] == (byte)06)
                            {
                                ackCode[0] = buffer[0];
                            }
                            if (buffer[0] == (byte)15)
                            {
                                ackCode[0] = buffer[0];
                            }
                            /*
                            if (buffer[0] == (byte)00)
                            {
                            } 
                            */
                            Console.WriteLine("ACK received: " + buffer[0]);
                            _serialPort.Write(ackCode, 0, ackCode.Length);
                            Console.WriteLine("End Reader. Timer starts");

                            aTimer = new System.Timers.Timer(3000);
                            aTimer.Start();
                            break;
                        case 1:
                            if (buffer[1] == (byte)02)
                                stx = buffer[1];
                            responseData.Add(stx);
                            break;
                        case 2:
                            len[0] = buffer[2];
                            responseData.Add(len[0]);
                            break;
                        case 3:
                            len[1] = buffer[3];
                            responseData.Add(len[1]);
                            break;
                        default:
                            if (i >= 4)
                            {
                                int dataIndex = i - 4;
                                int lenint = int.Parse(BitConverter.ToString(len).Replace("-", ""));  
                                if (dataIndex < lenint)
                                {
                                    //Console.WriteLine(i + ", " + buffer[4] + ", "+ buffer[i]);
                                    data.Add(buffer[i]);
                                    responseData.Add(buffer[i]);
                                }
                                if (dataIndex == lenint)
                                {
                                    etx = buffer[i];
                                    responseData.Add(etx);
                                }
                                if (dataIndex == lenint + 1)
                                {
                                    checksum[0] = buffer[i];
                                    byte lrc = c.ValidateLRC(len.Concat(data).ToArray(), lenint + 4); //c.GetLRC(buffer, stx, etx);
                                    //stop = true;
                                    Console.WriteLine(BitConverter.ToString(buffer).Replace("-", " "));
                                    aTimer.Stop();

                                    Thread.Sleep(3000);
                                    Console.WriteLine("ProcessResponse, timer stops");
                                    Console.WriteLine("Calculated LRC : " + checksum[0] + ", bit : " + BitConverter.ToString(checksum));
                                    Console.WriteLine("ResponseData Calculated LRC: " + lrc);

                                    if (lrc == checksum[0]) Console.WriteLine("LRC Valid!");
                                    else Console.WriteLine("LRC Invalid!");

                                    Console.WriteLine("Sending ACK : " + ackCode[0]);

                                    _continue = false;
                                    _serialPort.Write(ackCode, 0, ackCode.Length);      
                                    
                                    //_serialPort.DiscardInBuffer();
                                    ProcessData(responseData, modelName); 
                                }
                            }
                            break;
                    }
                }

                Thread.Sleep(3000);
            }
        }

        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.LastIndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        } 

        public byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        } 

    }

}
