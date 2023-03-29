using FinalProject.Models;

namespace FinalProject.Client.Models
{
    public class LoggerDto
    {
        public virtual Leg? Leg { get; set; }
        public virtual Flight? Flight { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }
    }
}
