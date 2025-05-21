using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NETSFUNCTION.Helper
{
    internal class CommonHelper
    {
        public List<byte> CalLength(MessageHeader messageHeader, List<MessageData> messageDataList)
        {

            List<byte> byteLength = new List<byte>();

            if (messageHeader != null)
            {
                foreach (PropertyInfo prop in messageHeader.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(byte[]))
                    {
                        byte[] content = prop.GetValue(messageHeader, null) as byte[];

                        foreach (byte b in content)
                        {
                            byteLength.Add(b);
                        }
                    }
                    else
                    {
                        byteLength.Add((byte)prop.GetValue(messageHeader));
                    }
                }
            }

            if (messageDataList != null)
            {
                foreach (MessageData messageData in messageDataList)
                {
                    if (messageData != null)
                    {
                        foreach (PropertyInfo prop in messageData.GetType().GetProperties())
                        {

                            if (prop.PropertyType == typeof(byte[]))
                            {
                                byte[] content = prop.GetValue(messageData, null) as byte[];

                                foreach (byte b in content)
                                {
                                    //Console.WriteLine($"{prop.Name}: {b}  array");
                                    byteLength.Add(b);
                                }
                            }
                            else if (prop.PropertyType == typeof(int))
                            {
                                int len = (int)prop.GetValue(messageData);
                                byte[] lenBytes = ConvertToBCD(len);
                                foreach (byte b in lenBytes)
                                {
                                    byteLength.Add(b);
                                    //Console.WriteLine($"{prop.Name} : {prop.GetValue(messageData)} string");
                                }
                            }
                            else
                            {
                                byteLength.Add((byte)prop.GetValue(messageData));
                                //Console.WriteLine($"{prop.Name} : {prop.GetValue(messageData)} normal");
                            }
                        }
                    }
                }

            }

            return byteLength;
        }
        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public byte ValidateLRC(byte[] bytes, int length)
        {
            Console.WriteLine("CheckMessage : " + BitConverter.ToString(bytes.ToArray()) + ", Length : " + length);
            byte B_SEP = (byte)0x1C;
            byte B_ETX = (byte)0x03;
            byte LRC = 0X00;
            for (int i = 0; i < length; i++)
            {
                LRC ^= bytes[i];
                if (bytes[i] == B_ETX)
                {
                    if (bytes[i - 1] == B_SEP)
                    {
                        break;
                    }
                } 
            }
            return LRC;
        }

        public byte GetLRC(byte[] bytes, byte B_STX, byte B_ETX)
        {
            //byte B_STX = (byte)0x02;    // Start of Text
            //byte B_NUL = (byte)0x00;
            //byte B_ETX = (byte)0x03;    // End of Text
            //byte B_ACK = (byte)0x06;    // Acknowledgement
            //byte B_NAK = (byte)0x15;    // Negative Acknowledgement
            //byte B_EOT = (byte)0x04;
            //byte B_ENQ = (byte)0x05;
            //byte B_ZERO = (byte)0x30;
            //byte B_ONE = (byte)0x31;
            byte LRC = 0X00;

            bool foundSTX = false;
            int startFindEtx = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (foundSTX)
                {
                    ++startFindEtx;
                    LRC ^= bytes[i];
                }
                if (bytes[i] == B_STX)
                {
                    foundSTX = true;
                }

                if (startFindEtx > 3 && bytes[i] == B_ETX)
                {
                    break;
                }
            }
            return LRC;
        }

        public byte[] ConvertToBCD(int value)
        {
            byte[] bcdBytes = new byte[2];
            for (int i = 1; i >= 0; i--)
            {
                bcdBytes[i] = (byte)(value % 10);
                value /= 10;
                bcdBytes[i] |= (byte)(value % 10 << 4);
                value /= 10;
            }
            return bcdBytes;
        }


        public byte[] ToBytes(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            StringBuilder hexNumbers = new StringBuilder();
            foreach (byte b in bytes)
            {
                //Console.WriteLine(b); //in bytes
                hexNumbers.Append(b.ToString("x").Insert(0, "0X") + " "); //in hexa
            }
            //Console.WriteLine(hexNumbers.ToString());

            return bytes;
        }

        public byte ToByte(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            byte a = 0;
            StringBuilder hexNumbers = new StringBuilder();
            foreach (byte b in bytes)
            {
                //Console.WriteLine(b); //in bytes
                a = b; //in hexa
            }
            //Console.WriteLine(hexNumbers.ToString());

            return a;
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

        public byte[][] separate(byte[] source, byte[] separator)
        {
            var parts = new List<byte[]>();
            var index = 0;
            byte[] part;
             
            for (var i = 0; i < source.Length; ++i)
            {
                if (equals(source, separator, i))
                {
                    part = new byte[i - index];
                    Array.Copy(source, index, part, 0, part.Length);
                    parts.Add(part);
                    index = i + separator.Length;
                    i += separator.Length - 1;
                }
            }
            part = new byte[source.Length - index];
            Array.Copy(source, index, part, 0, part.Length);
            parts.Add(part);
            return parts.ToArray();
        }

        bool equals(byte[] source, byte[] separator, int index)
        {
            for (int i = 0; i < separator.Length; ++i)
                if (index + i >= source.Length || source[index + i] != separator[i])
                    return false;
            return true;
        }

        /*  */

        public string GetECN()
        {
            string Result = Convert.ToString(DateTime.Now.Year.ToString().Substring(2, 2) + "" +
                                DateTime.Now.Month.ToString("D2") + "" +
                                DateTime.Now.Day.ToString("D2") + "" +
                                DateTime.Now.Hour.ToString("D2") + "" +
                                DateTime.Now.Minute.ToString("D2") + "" +
                                DateTime.Now.Second.ToString("D2"));
            return Result;
        }


        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        
        public string ConvertUnicode(string len)
        {
            string unicodeString = null;
            //onedigit alphabet
            if (Regex.IsMatch(len, "^[A-Fa-f]{1}$"))
                unicodeString = String.Format("\\{0}", len);

            //TwoDigit alphanumeric
            if (Regex.IsMatch(len, "^[0-9A-Fa-f]{2}$"))
                unicodeString = String.Format("\\u{0}", len.ToString().PadLeft(5, '0'));

            //OneDigith , TwoDigit number
            if (Regex.IsMatch(len, "^[0-9]{2}$") || Regex.IsMatch(len, "^[0-9]{1}$"))
                unicodeString = String.Format("\\u{0}", len.ToString().PadLeft(4, '0'));

            return unicodeString;
        }

        public Dictionary<string, string> PairFieldCode(string[] fieldNames)
        {
            Dictionary<string,string> fcPair = new Dictionary<string, string>();
            string fieldCode = null;
            for (int index = 0; index < fieldNames.Length; index++)
            {
                fieldCode = (string)typeof(FieldCodeHelper).GetField(fieldNames[index]).GetValue(typeof(FieldCodeHelper));
                fcPair.Add(fieldNames[index], fieldCode);
            }
            return fcPair;
        }

    }
}
