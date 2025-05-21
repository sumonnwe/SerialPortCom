using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    internal class NetsPurchaseResponseModel
    {
        public NetsPurchaseResponseModel() { }
        public string ResponseText { get; set; } //40
        public string MerchantNameAndAddress { get; set; } //69 
        public string TransactionDate { get; set; } //6
        public string TransactionTime { get; set; } //6
        public string TerminalID { get; set; } //8
        public string MerchantID { get; set; } //15
        public string STAN { get; set; } //6
        public string ApprovalCode { get; set; } //6

        public string RetrievalRefNum { get; set; } = "";


        public string CardName { get; set; } //10
        public string TransactionAmt { get; set; } //12
        public string CashBackAmt { get; set; } //12
        public string ServiceFee { get; set; } //12
        public string POSMsg { get; set; } //240
        public string ResponseMsg1 { get; set; } //20
        public string ResponseMsg2 { get; set; } //20
        public string LoyaltyPgmName { get; set; } //24
        public string LoyaltyPgmExpDate { get; set; } //8
        public string LoyaltyType { get; set; } //1
        public string LoyaltyMktMsg { get; set; } //144
        public string RedemptionVal { get; set; } //12
        public string CurrentLoyaltyBal { get; set; } //12
        public string HostRespCode { get; set; } //2
        public string CardEntryMode { get; set; } //2
        public string ECRRefNum { get; set; } //13
        public string ReceiptTxtFormat { get; set; } //VAR
    }
}
