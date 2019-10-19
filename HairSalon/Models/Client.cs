using System.Collections.Generic;

namespace Salon.Models
{
    public class Client
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int StylistId { get; set; }
        public virtual Stylist Stylist { get; set; }
    }
}