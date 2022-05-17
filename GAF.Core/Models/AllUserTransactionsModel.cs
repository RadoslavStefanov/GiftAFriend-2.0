using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAF.Core.Models
{
    public class AllUserTransactionsModel
    {
        public DateTime DateTime { get; set; }
        public string senderUserName { get; set; }
        public string recieverUserName { get; set; }
        public decimal Amount { get; set; }
    }
}
