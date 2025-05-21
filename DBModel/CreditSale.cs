using System;
using System.Collections.Generic;

namespace NETSFUNCTION.DBModel;

public partial class CreditSale
{
    public int Id { get; set; }

    public string? Ack { get; set; }

    public string? ResponseHeader { get; set; }

    public string? ResponseText { get; set; }

    public string? MerchantNameAndAddress { get; set; }

    public string? TransactionDate { get; set; }

    public string? TransactionTime { get; set; }

    public string? ApprovalCode { get; set; }

    public string? InvoiceNum { get; set; }

    public string? TerminalId { get; set; }

    public string? MerchantId { get; set; }

    public string? CardIssuerName { get; set; }

    public string? CardType { get; set; }

    public string? CardNumber { get; set; }

    public string? ProcessingGateway { get; set; }

    public string? CardDescription { get; set; }

    public string? ExpiryDate { get; set; }

    public string? BatchNum { get; set; }

    public string? RetrievalRefNum { get; set; }

    public string? CardHolderName { get; set; }

    public string? Aid { get; set; }

    public string? AppProfile { get; set; }

    public string? Cid { get; set; }

    public string? TransactionCert { get; set; }

    public string? Tsi { get; set; }

    public string? Tvr { get; set; }

    public string? TransactionId { get; set; }

    public string? Posid { get; set; }

    public string? HostRespCode { get; set; }

    public string? CardEntryMode { get; set; }

    public string? EcrrefNum { get; set; }

    public string? LoyaltyType { get; set; }

    public string? LoyaltyInfo { get; set; }

    public string? SchemeCategory { get; set; }

    public string? ReceiptTxtFormat { get; set; }

    public DateTime? CreatedDateTime { get; set; }
}
