using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class SORUser : IdentityUser
    {
        public virtual ICollection<Reservation> Reservation { get; set; }
        public long ReservationId { get; set; }
    }
}
