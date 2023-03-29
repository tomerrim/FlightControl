namespace FinalProject.Client.Models
{
    public class FlightDto
    {
        public int PassengersCount { get; set; }
        public bool IsCritical { get; set; }
        public string? Brand { get; set; }
        public bool IsDeparture { get; set; }
    }
}
