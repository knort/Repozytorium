using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model
{
    public class Table
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TableId { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
