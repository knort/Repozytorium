using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Model.ViewModels
{
    public class TableViewModel
    {
        public Guid TableId { get; set; }
        [Required]
        public int Number { get; set; }
        public ICollection<ReservationViewModel> Reservation { get; set; }
    }
}
