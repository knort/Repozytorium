using AutoMapper;
using SOR.Model;
using SOR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Api.MappingProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<SORUser, UserViewModel>()
            .ForMember(uvm => uvm.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(uvm => uvm.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(uvm => uvm.Reservation, opt => opt.MapFrom(src => src.Reservation))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UserViewModel, SORUser>()
            .ForMember(uvm => uvm.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
            .ForMember(uvm => uvm.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(uvm => uvm.Reservation, opt => opt.MapFrom(src => src.Reservation))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Reservation, ReservationViewModel>()
            .ForMember(src => src.User, opt => opt.MapFrom(src => src.SORUser))
            .ForMember(vm => vm.UserId, opt => opt.MapFrom(src => src.SORUserId));

            CreateMap<ReservationViewModel, Reservation>()
            .ForMember(src => src.SORUser, opt => opt.MapFrom(src => src.User))
            .ForMember(vm => vm.SORUserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<TableViewModel, Table>();
        }
    }
}
