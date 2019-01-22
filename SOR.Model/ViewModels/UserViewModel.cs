using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOR.Model.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public ICollection<ReservationViewModel> Reservation { get; set; }
        public Guid ReservationId { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}