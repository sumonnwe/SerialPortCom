using System;
using System.Collections.Generic;

namespace NETSFUNCTION.DBModel
{

    public partial class NetsPurchase
    {
        public int Id { get; set; }

        public string? Ack { get; set; }

        public string? ResponseHeader { get; set; }

        public string? ResponseText { get; set; }

        public string? MerchantNameAndAddress { get; set; }

        public string? TransactionDate { get; set; }

        public string? TransactionTime { get; set; }

        public string? TerminalId { get; set; }

        public string? MerchantId { get; set; }

        public string? Stan { get; set; }

        public string? ApprovalCode { get; set; }

        public string? RetrievalRefNum { get; set; }

        public string? CardName { get; set; }

        public string? TransactionAmt { get; set; }

        public string? CashBackAmt { get; set; }

        public string? ServiceFee { get; set; }

        public string? Posmsg { get; set; }

        public string? ResponseMsg1 { get; set; }

        public string? ResponseMsg2 { get; set; }

        public string? LoyaltyPgmName { get; set; }

        public string? LoyaltyPgmExpDate { get; set; }

        public string? LoyaltyType { get; set; }

        public string? LoyaltyMktMsg { get; set; }

        public string? RedemptionVal { get; set; }

        public string? CurrentLoyaltyBal { get; set; }

        public string? HostRespCode { get; set; }

        public string? CardEntryMode { get; set; }

        public string? EcrrefNum { get; set; }

        public string? ReceiptTxtFormat { get; set; }

        public DateTime? CreatedDateTime { get; set; }
    }


}