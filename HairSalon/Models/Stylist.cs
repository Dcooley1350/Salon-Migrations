using System.Collections.Generic;

namespace Salon.Models
{
    public class Stylist
    {
        public Stylist()
        {
            this.Clients = new HashSet<StylistClient>();
        }
        public string Specialty { get; set; }
        public string Name { get; set; }
        public int StylistId {get; set; }
        public virtual ICollection<StylistClient> Clients { get; set; }

    }
}