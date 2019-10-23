using System.Collections.Generic;

namespace Salon.Models
{
    public class Client
    {
        public Client()
        {
            this.Stylists = new HashSet<StylistClient>();
        }

        public string Name { get; set; }
        public int ClientId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<StylistClient> Stylists { get; set; }
    }
}