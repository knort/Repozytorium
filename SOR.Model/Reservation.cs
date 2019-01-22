using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public Guid TableId { get; set; }
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        public string SORUserId { get; set; }
        [ForeignKey("SORUserId")]
        public virtual SORUser SORUser { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
