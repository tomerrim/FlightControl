using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int WaitTime { get; set; }
        public virtual Flight? Flight { get; set; }
        public LegNumber CurrentLeg { get; set; }
        public LegNumber NextLegs { get; set; }
        public bool IsChangeStatus { get; set; }
    }
}
