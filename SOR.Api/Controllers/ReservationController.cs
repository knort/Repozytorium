using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOR.BLL;
using SOR.Model;
using SOR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.Constants.USER_POLICY)]
    public class ReservationController : SORBaseController<Reservation, ReservationViewModel>
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper) : base(reservationService, mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost("tableReservations")]
        public async Task<IActionResult> GetTableReservationsByDate([FromBody]TableReservationViewModel tableReservationViewModel)
        {
            var reservations = await _reservationService.GetTableReservationsByDate(tableReservationViewModel.tableId, tableReservationViewModel.Date);
            if (reservations.Count > 0)
            {
                var reservationViewModels = _mapper.Map<ICollection<ReservationViewModel>>(reservations);
                return Ok(reservationViewModels);
            }
            return NoContent();
        }

        [HttpGet("userReservations/{userId}")]
        public async Task<IActionResult> GetUserReservations(string userId)
        {
            var reservations = await _reservationService.GetUserReservations(userId);
            if (reservations.Count > 0)
            {
                var reservationViewModels = _mapper.Map<ICollection<ReservationViewModel>>(reservations);
                return Ok(reservationViewModels);
            }
            return NoContent();
        }

        public async override Task<IActionResult> PostAsync([FromBody]ReservationViewModel viewmodel)
        {
            var reservation = _mapper.Map<Reservation>(viewmodel);
            var canCreate = await _reservationService.CanReservateInThisTime(reservation);
            if (canCreate)
                return await base.PostAsync(viewmodel);
            return BadRequest(new { error = "Nie możesz zarezerwować tego stolika o tych godzinach!" });
        }

        [HttpGet("last/{userId}")]
        public async Task<IActionResult> GetLastReservation(string userId)
        {
            var reservation = await _reservationService.GetLastReservation(userId);
            if(reservation != null)
            {
                var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);
                return Ok(reservationViewModel);
            }
            return NoContent();
        }


        [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
        public override async Task<IActionResult> UpdateAsync([FromBody] ReservationViewModel viewmodel)
        {
            return await base.UpdateAsync(viewmodel);
        }
    }
}
