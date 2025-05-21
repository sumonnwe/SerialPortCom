using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataResponseModel
{
    public class ResponseMessageHeader
    {
        public ResponseMessageHeader() { }

        public string ECN { get; set; }

        public string FunctionCode { get; set; }

        public string ResponseCode { get; set; }

        public string RFU { get; set; }
    }
}
