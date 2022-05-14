using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAF.Infrastructure.Data.Models
{
    public class TransferEvents
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public int CallerId { get; set; }
        public int CallerName { get; set; }
        public int RecieverId { get; set; }
        public int RecieverName { get; set; }
        public string Message { get; set; }
    }
}
