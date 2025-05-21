using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSFUNCTION.DataRequestModel
{
    internal class CreditSaleModel
    {
        public CreditSaleModel() { }

        public MessageData TransactionAmount { get; set; }  
        public MessageData AcquireName { get; set; }

        public MessageData EnhancedECRNum { get; set; } 

        public MessageData SchemeCategory { get; set; }
    }
}
