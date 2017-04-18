using AutoMapper;
using FullStackPluralsight.Dtos;
using FullStackPluralsight.Models;

namespace FullStackPluralsight.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>(); 
        }

        protected override void Configure()
        {
            //throw new NotImplementedException();
        }
    }
}