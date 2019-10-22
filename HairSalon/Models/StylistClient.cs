namespace Salon.Models
{
    public class StylistClient
    {
        public int StylistClientId { get; set; }
        public int ClientId { get; set; }
        public int StylistId { get; set; }
        public Client Client { get; set; }
        public Stylist Stylist { get; set; }
    }
}