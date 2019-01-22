using SOR.Model;
using SOR.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.BLL
{
    public class ReservationService : BaseService<Reservation>, IReservationService
    {
        private readonly IRepository<Reservation> _repository;

        public ReservationService(IRepository<Reservation> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<Reservation>> GetTableReservationsByDate(Guid id, DateTime day)
        {
            return await _repository.GetAllCustomsAsync(res => res.Date.Date == day.Date && res.TableId == id);
        }

        public async Task<ICollection<Reservation>> GetUserReservations(string id)
        {
            return await _repository.GetAllCustomsAsync(res => res.SORUserId == id);
        }

        public async Task<bool> CanReservateInThisTime(Reservation reservation)
        {
            var conflicts = await _repository.GetAllCustomsAsync(res =>
            (res.Date.Date == reservation.Date.Date)
            && (res.TableId == reservation.TableId)
            && (
            (reservation.Start > res.Start && res.End > reservation.End) || (reservation.Start.TimeOfDay == res.Start.TimeOfDay && reservation.End.TimeOfDay == res.End.TimeOfDay) || (reservation.Start > res.Start && reservation.Start < res.End) || (res.Start < reservation.Start && res.End > reservation.End) || (reservation.Start < res.Start && reservation.End > res.End) || (reservation.Start < res.Start && reservation.End > res.Start)
            )
            );

            if (conflicts.Count > 0)
                return false;
            return true;
        }

        public async Task<Reservation> GetLastReservation(string userId)
        {
            var allReservations = await _repository.GetAllCustomsAsync(reservation => reservation.SORUserId == userId);
            return allReservations.OrderByDescending(res => res.Date).FirstOrDefault();
        }
    }
}
