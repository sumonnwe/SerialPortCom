namespace NETSFUNCTION.DataRequestModel
{
    internal class NetsPurchaseModel
    {

        public NetsPurchaseModel() { }

        public MessageData TransactionTypeInd { get; set; } //T2
        public MessageData TransactionAmt { get; set; } //40
        public MessageData CashBackAmt { get; set; } //42         
        public MessageData ECRNo { get; set; } //HD
         
    }
} 