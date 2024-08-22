using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Message
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
