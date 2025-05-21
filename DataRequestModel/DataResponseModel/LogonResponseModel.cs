using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    public class LogonResponseModel
    {
        public LogonResponseModel() { }   
        public string ResponseText { get; set; } //40
        public string MerchantNameAndAddress { get; set; } //69  
        public string TransactionDate { get; set; } //6
        public string TransactionTime { get; set; } //6

        public string STAN { get; set; } //6 
        public string TerminalID { get; set; } //8
        public string MerchantID { get; set; } //15  
        public string HostRespCode { get; set; } //2 
    }
}
