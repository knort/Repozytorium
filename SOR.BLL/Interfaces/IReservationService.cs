using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOR.Model;

namespace SOR.BLL
{
    public interface IReservationService : IBaseService<Reservation>
    {
        Task<ICollection<Reservation>> GetTableReservationsByDate(Guid id, DateTime day);
        Task<ICollection<Reservation>> GetUserReservations(string id);
        Task<bool> CanReservateInThisTime(Reservation reservation);
        Task<Reservation> GetLastReservation(string userId);
    }
}