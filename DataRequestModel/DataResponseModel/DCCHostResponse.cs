using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    public class DCCHostResponse
    {
        public DCCHostResponse() { }


        /// <summary>
        /// This entry will be repeated depending on the value at field code 9L Number of DCC Host
        /// </summary>
        public string HostID { get; set; }
        public string CurrencyCode { get; set; }
        public string DataType { get; set; }
        public string SaleCount { get; set; }
        public string SaleTotal { get; set; }
    }
}
