namespace NETSFUNCTION.Helper
{
    internal class FunctionCodeHelper
    {
        public static readonly string StatusCheck = "55"; //new byte[] { 0X35, 0X35 }; //Request Terminal Status (Function Code 55)

        public static readonly string LastApprvTxn = "56"; //new byte[] { 0X35, 0X36 };    //Get Last Approved Transaction (Function Code 56)

        public static readonly string Logon = "80";  //new byte[] { 0X38, 0X30 };    //Logon (Function Code 80)

        public static readonly string Settlement = "81"; //new byte[] { 0X38, 0X31 }; //Acquire Settlement (Function Code 81)

        public static readonly string AcqLogon = "A1"; //new byte[] { 0X41, 0X31 }; //Acquire AcqLogon (Function Code A1)

        public static readonly string AcqSettlement = "A2"; //new byte[] { 0X41, 0X32 }; //Acquire AcqSettlement (Function Code A2)

        public static readonly string AcqPreSettlement = "A3"; //new byte[] { 0X41, 0X33 }; //AcqPreSettlement (Function Code A3)

        public static readonly string PreSettlement = "86"; //new strbyte[]ing { 0X38, 0X36 }; //Settlement (Function Code 86)

        public static readonly string GetLastSettlementReceipt = "85"; //new byte[] { 0X38, 0X35 }; //GetLastSettlementReceipt (Function Code 85)

        public static readonly string Reversal = "14";  //new byte[] { 0X31, 0X34 }; //Reversal (Function Code 14)

        public static readonly string SetNETSParam = "82"; //new byte[] { 0X38, 0X32 }; //SetNETSParam (Function Code 82)

        public static readonly string GetNETSParam = "83"; //new byte[] { 0X38, 0X33 }; //SetNETSParam (Function Code 83)

        public static readonly string TMSConnect = "84"; //new byte[] { 0X38, 0X34 }; //GetNETSParam (Function Code 84)

        public static readonly string ConnectivityTest = "8A"; //new byte[] { 0X38, 0X41 }; //SetNETSParam (Function Code 8A)

        public static readonly string NETSCashCardTopup = "45"; //new byte[] { 0X34, 0X35 }; //NETS CashCard Top Up (Function Code 45)

        public static readonly string NETSPurchase = "30"; //new byte[] { 0X33, 0X30 }; //All NETS Purchase (Function Code 30)

        public static readonly string NETSVoid = "13"; //new byte[] { 0X31, 0X33 }; //NETS Void  (Function Code 13)

        public static readonly string CashDeposit = "17"; //new byte[] { 0X31, 0X37 }; //Cash Deposit  (Function Code 17)

        public static readonly string UnionPayPurchase = "31"; //new byte[] { 0X33, 0X31 }; //UnionPay Purchase  (Function Code 31)

        public static readonly string UnionPayVoid = "91"; //new byte[] { 0X39, 0X31 }; //UnionPay Void  (Function Code 91)

        public static readonly string UnionPayRefund = "99"; //new byte[] { 0X39, 0X39 }; //UnionPay Refund  (Function Code 99)

        public static readonly string BCAPurchase = "65"; //new byte[] { 0X36, 0X35 }; //BCA Purchase  (Function Code 65)

        public static readonly string BCAVoid = "68"; //new byte[] { 0X36, 0X38 }; //BCA Void  (Function Code 68)

        public static readonly string NFPTopup = "46"; //new byte[] { 0X34, 0X36 }; //NFP TopUP  (Function Code 46)

        public static readonly string CreditCardSale = "I0"; //new byte[] { 0X49, 0X30 }; //Credict Card Sale  (Function Code I0)

        public static readonly string CreditCardVoid = "I1"; //new byte[] { 0X49, 0X31 }; //Credict Card Void  (Function Code I1)

        public static readonly string CredictCardRefund = "I4"; //new byte[] { 0X49, 0X31 }; //Credict Card Refund  (Function Code I4)

        public static readonly string CredictCardSettlement = "I5"; //new byte[] { 0X49, 0X35 }; //Credict Card Settlement  (Function Code I5)
        public static readonly string CredictCardPreSettlement = "I6"; //new byte[] { 0X49, 0X35 }; //Credict Card Settlement  (Function Code I6)

        public static readonly string SignatureImage = "IA"; //new byte[] { 0X49, 0X41 }; //Signature Image  (Function Code IA)

        public static readonly string CredictCardIPPSale = "I7"; //new byte[] { 0X49, 0X37 }; //Credit Card IPP SALE  (Function Code I7)

        public static readonly string CreditCardIPPVOID = "I8"; //new byte[] { 0X49, 0X38 }; //Credit Card IPP VOID  (Function Code I8)

        public static readonly string ReadTrack2Data = "S0"; //new byte[] { 0X53, 0X30 }; //Read Track 2 Data  (Function Code S0)

        public static readonly string GetCardInfo = "S1"; //new byte[] { 0X53, 0X31 }; //GetCardInfo  (Function Code S1)

        public static readonly string SingleEntryPurchase = "00"; //new byte[] { 0X30, 0X30 }; //Single Entry Purchase  (Function Code 00)

        public static readonly string NPCBalanceInquiry = "3A"; //new byte[] { 0X33, 0X41 }; //NPC Balance Inquiry  (Function Code 3A)

        public static readonly string NPCTopUp = "3B"; //new byte[] { 0X33, 0X42 }; //NPC Top Up  (Function Code 3B)


    }
}
