using NETSFUNCTION;
using NETSFUNCTION.DataRequestModel;
using NETSFUNCTION.DataResponseModel;
using NETSFUNCTION.Helper; 
using System.IO.Ports;
using NLog;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Drawing;

public class Program
{
    private static string commPort = "COM4"; //System.Configuration.ConfigurationManager.AppSettings["COMMPORT"];
    private static SerialConnHandler? serialCon = new SerialConnHandler(commPort);

    private static Logger logger = LogManager.GetCurrentClassLogger(); 

    public static void Main(string[] args)
    {
         
        Console.WriteLine("============== Request Start with COM SERIAL PORT: " );

        CommonHelper com = new CommonHelper();

        //ExecFunction(FunctionCodeHelper.TMSConnect, commPort, null, null);
        //ExecFunction(FunctionCodeHelper.ConnectivityTest, commPort, null, null);
        //ExecFunction(FunctionCodeHelper.Logon, commPort, null, typeof(LogonResponseModel)); 
        
        List<MessageData> netsPurchaseRequest = new List<MessageData>();
        NetsPurchaseModel netsPurchaseModel = new NetsPurchaseModel();
        netsPurchaseModel.TransactionTypeInd = new MessageData { FieldCode = com.ToBytes("T2"), Len = 2, Data = com.ToBytes("01"), Separator = 0X1C };
        netsPurchaseModel.TransactionAmt = new MessageData { FieldCode = com.ToBytes("40"), Len = 12, Data = com.ToBytes("000000000001"), Separator = 0X1C };
        netsPurchaseModel.CashBackAmt = new MessageData { FieldCode = com.ToBytes("42"), Len = 12, Data = com.ToBytes("000000000000"), Separator = 0X1C };
        netsPurchaseModel.ECRNo = new MessageData { FieldCode = com.ToBytes("HD"), Len = 13, Data = com.ToBytes("1234567890123"), Separator = 0X1C };
        netsPurchaseRequest.Add(netsPurchaseModel.TransactionTypeInd);
        netsPurchaseRequest.Add(netsPurchaseModel.TransactionAmt);
        netsPurchaseRequest.Add(netsPurchaseModel.CashBackAmt);
        netsPurchaseRequest.Add(netsPurchaseModel.ECRNo);
        ExecFunction(FunctionCodeHelper.NETSPurchase, commPort, netsPurchaseRequest, typeof(NetsPurchaseResponseModel));
        
    }

    static async void ExecFunction(string funcCode, string commPort, List<MessageData> messageDataList = null, Type modelName = null)
    {

        Console.WriteLine("========== Execute Function Code = " + funcCode);
        //-------------------------------------------- 1 - Prepare Command Bytes
        byte[] commandBytes = PrepareCommand(funcCode, messageDataList);


        //-------------------------------------------- 2 - Serial Port Conn 
        serialCon.Start(modelName, commandBytes);
        serialCon.Read(modelName);   
    }

    static byte[] PrepareCommand(string funcCode, List<MessageData> messageDataList)
    {
        List<byte> requestData = new List<byte>();
        CommonHelper com = new CommonHelper();

        byte stx = (byte)0x02;
        byte etx = (byte)0x03;
        int msgLength = 0;
        List<byte> msgBytes = new List<byte>();

        MessageHeader messageHeader = new MessageHeader
        {
            ECN = com.ToBytes(com.GetECN()), //"000000005764",
            FunctionCode = com.ToBytes(funcCode), //FunctionCodeHelper.LastApprvTxn,    //new byte[] { 0X99, 0X99 };
            VersionCode = com.ToBytes("01"), //VersionCodeHelper.defaultVer,   // new byte[] { 0X99, 0X99 };
            RFU = com.ToByte("0"), //0X30
            Seperator = 0X1C //0X1C
        };
        //Message Header, Message Data Length
        if (messageDataList != null)
        {            
            msgLength = com.CalLength(messageHeader, messageDataList).ToArray().Length;
        }
        else
        {
            msgLength = com.CalLength(messageHeader, null).ToArray().Length;
        }
        
        byte[] msgLengthBCD = com.ConvertToBCD(msgLength);
        //Message Header, Message Data Byte
        if (messageDataList != null)
        { 
            msgBytes = com.CalLength(messageHeader, messageDataList);
        }
        else
        {
            msgBytes = com.CalLength(messageHeader, null);
        }

        //Set Request Data Format 
        //0:stx
        requestData.Add(stx);
        //1:length
        foreach (byte b in msgLengthBCD)
        {
            requestData.Add(b);
        }
        //2:Message header , Message Data
        foreach (byte b in msgBytes)
        {
            requestData.Add(b);
        }
        //3:etx
        requestData.Add(etx);
        //4:lrc
        byte lrc = com.GetLRC(requestData.ToArray(), stx, etx);
        requestData.Add(lrc);


        string data = BitConverter.ToString(requestData.ToArray()).Replace("-", "");
        Console.WriteLine("message request data : " + data);

        logger.Debug("Request Message *** " + data);
        logger.Debug("Request Message in hexa format *** " + BitConverter.ToString(Encoding.Default.GetBytes(data)).Replace("-", ""));


        return requestData.ToArray(); 


    }

}

