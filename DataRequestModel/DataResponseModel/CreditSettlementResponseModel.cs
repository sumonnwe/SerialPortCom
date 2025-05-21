using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NETSFUNCTION.DataResponseModel
{
    public class CreditSettlementResponseModel
    {
        public CreditSettlementResponseModel() { }
        public string ResponseText { get; set; } //40
        public string MerchantNameAndAddress { get; set; } //69  
        public string POSID { get; set; } //8 
        public string TransactionDate { get; set; } //6
        public string TransactionTime { get; set; } //6 
        public string HostRespCode { get; set; } //2   HC 
        public string NumOfCreditHost { get; set; } //2  9J

        public List<CreditHostResponse>? CreditHost { get; set; }

        public string NumOfIPPHost { get; set; } //2  9P
         
        public List<IPPHostResponse>? IPPHost { get; set; }

        public string NumOfDCCHost { get; set; }    //2  9L

        public List<DCCHostResponse>? DCCHost { get; set; }

    }
}


