using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    public class SettlementResponseModel
    {
        public SettlementResponseModel() { }   
        public string ResponseText { get; set; } //40
        public string MerchantNameAndAddress { get; set; } //69  
        public string TransactionDate { get; set; } //6
        public string TransactionTime { get; set; } //6

        public string STAN { get; set; } //6 
        public string TerminalID { get; set; } //8
        public string MerchantID { get; set; } //15  
        public decimal TotalNetsPurchaseCount { get; set; } //2 
        public decimal TotalNetsPurchaseAmount { get; set; } //6
        public decimal TotalVoidNetsPurchaseCount { get; set; } //2
        public decimal TotalVoidNetsPurchaseAmount { get; set; } //6

        public decimal TotalUnionPayPurchaseCount { get; set; } //2
        public decimal TotalUnionPayPurchaseAmount { get; set; } //6

        public decimal TotalVoidUnionPayPurchaseCount { get; set; } //2
        public decimal TotalVoidUnionPayPurchaseAmount { get; set; } //6
        public decimal TotalUnionPayRefundCount { get; set; } //2
        public decimal TotalUnionPayRefundAmount { get; set; } //6
        public decimal TotalUnionPayPreAuthCompletionCount { get; set; } //2
        public decimal TotalUnionPayPreAuthCompletionAmount { get; set; } //6
        public decimal TotalVoidUnionPayPreAuthCompletionCount { get; set; } //2
        public decimal TotalVoidUnionPayPreAuthCompletionAmount { get; set; } //6
        public decimal TotalBCAPurchaseCount { get; set; } //2
        public decimal TotalBCAPurchaseAmount { get; set; } //6
        public decimal TotalVoidBCAPurchaseCount { get; set; } //2
        public decimal TotalVoidBCAPurchaseAmount { get; set; } //6
        public decimal TotalBCAPreAuthCompletionCount { get; set; } //2
        public decimal TotalBCAPreAuthCompletionAmount { get; set; } //6
        public decimal TotalNFPCount { get; set; } //2
        public decimal TotalNFPAmount { get; set; } //6
        public decimal TotalNCCCount { get; set; } //2
        public decimal TotalNCCAmount { get; set; } //6
        public decimal TotalNFPTopupbyNetsCount { get; set; } //2
        public decimal TotalNFPTopupbyNetsAmount { get; set; } //6
        public decimal TotalNCCTopupbyNetsCount { get; set; } //2
        public decimal TotalNCCTopupbyNetsAmount { get; set; } //6
        public decimal TotalCashbackCount { get; set; } //2
        public decimal TotalCashbackAmount { get; set; } //6
        public decimal TotalCashDepositCount { get; set; } //2
        public decimal TotalCashDepositAmount { get; set; } //6
        public decimal TotalNPCTopbyNetsCount { get; set; } //2
        public decimal TotalNPCTopbyNetsAmount { get; set; } //6
        public decimal TotalNPCTopbyCashCount { get; set; } //2
        public decimal TotalNPCTopbyCashAmount { get; set; } //6
        public decimal TotalNPCActivationCount { get; set; } //2
        public string ReceiptTxtFormat { get; set; } //VAR
    }
}
