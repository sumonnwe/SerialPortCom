using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    public class CreditHostResponse
    {
        public CreditHostResponse() { }

        //Host Data Start
        /// <summary>
        /// This entry will be repeated depending on the value at field code 9J Number of Credit Host
        /// </summary>
        public string HostID { get; set; } //2 
        public string TerminalID { get; set; } //8 
        public string MerchantID { get; set; } //15 
        public string CardType { get; set; } //99 
        public string DataType { get; set; } //99 
        public string SaleCount { get; set; } //4 
        public string SaleTotal { get; set; } //12 
    }
}
