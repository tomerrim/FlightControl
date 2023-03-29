using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int PassengersCount { get; set; }
        public bool IsCritical { get; set; }
        public string? Brand { get; set; }
        public bool IsDeparture { get; set; }
    }
}
