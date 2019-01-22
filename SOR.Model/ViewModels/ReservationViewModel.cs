using System;
using System.ComponentModel.DataAnnotations;

namespace SOR.Model.ViewModels
{
    public class ReservationViewModel
    {
        public Guid ReservationId { get; set; }
        [Required]
        public Guid TableId { get; set; }
        public virtual TableViewModel Table { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
    }
}