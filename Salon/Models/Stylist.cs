using System.Collections.Generic;
namespace Salon.Models
{
    public class Stylist
    {
        public Stylist()
        {
            this.Clients = new HashSet<Client>();
        }
        public string Specialty { get; set; }
        public string Name { get; set; }
        public int StylistID {get; set; }
        public virtual ICollection<Client> Clients { get; set; }

    }
}