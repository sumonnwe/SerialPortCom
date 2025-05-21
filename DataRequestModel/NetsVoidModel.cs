namespace NETSFUNCTION.DataRequestModel
{
    internal class NetsVoidModel
    {
        public NetsVoidModel() { }

        //Options1
        public MessageData TerminalID { get; set; } //16
        public MessageData TransactionDate { get; set; } //03
        public MessageData TransactionTime { get; set; } //04    
        public MessageData TransactionAmt { get; set; } //40 
        //Options1,//Options2
        public MessageData OriginalTransactionSTAN { get; set; } //65 
        public MessageData ECRRefNum { get; set; } //H4 
         

    }
}
