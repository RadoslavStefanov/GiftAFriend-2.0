using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAF.Core.Models
{
    public class GiftSendPostModel
    {
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string? Message { get; set; }
    }
}
