using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    internal class CreditSaleResponseModel
    {
        public CreditSaleResponseModel() { }
        public string ResponseText { get; set; } //40
        public string MerchantNameAndAddress { get; set; } //69 
        public string TransactionDate { get; set; } //6
        public string TransactionTime { get; set; } //6
        public string TerminalID { get; set; } //8
        public string MerchantID { get; set; } //15 

        public string CardIssuerName { get; set; }  //20
        public string CardType { get; set; }    //20
        public string CardNumber { get; set; }  //19
        public string ProcessingGateway { get; set; } //20

        public string CardDescription { get; set; } //20
        public string ExpiryDate { get; set; }  //4
        public string BatchNum { get; set; } //6
        public string RetrievalRefNum { get; set; } //12

        public string CardHolderName { get; set; } //26
        public string AID { get; set; } //16

        public string AppProfile { get; set; } //16

        public string CID { get; set; } //2

        public string TransactionCert { get; set; } //16

        public string TSI { get; set; } //4

        public string TVR {  get; set; } //10

        public string TransactionID { get; set; } //10
        public string POSID { get; set; } //8
        public string HostRespCode { get; set; } //2
        public string CardEntryMode { get; set; } //2
        public string ECRRefNum { get; set; } //13
        public string LoyaltyType { get; set; } //2
        public string LoyaltyInfo { get; set; } //94

        public string SchemeCategory { get; set; } //20

        //public string OfflineTxnType { get; set; } //1
        public string ReceiptTxtFormat { get; set; } //var
    }
}
