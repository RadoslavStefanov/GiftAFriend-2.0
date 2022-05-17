using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAF.Core.Models
{
    public class UserTransactionsModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string FromToUser { get; set; }
        public string FromToUserId { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }

    }
}
