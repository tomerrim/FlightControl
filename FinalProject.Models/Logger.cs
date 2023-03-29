using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Logger
    {
        public int Id { get; set; }
        public virtual Leg? Leg { get; set; }
        public virtual Flight? Flight { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }
    }
}
