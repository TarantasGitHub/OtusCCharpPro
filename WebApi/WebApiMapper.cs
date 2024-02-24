using AutoMapper;
using ClassLibraryContracts.Models;
using WebApi.Models;

namespace WebApi
{
    public class WebApiMapper : Profile
    {
        public WebApiMapper()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.Lastname))
                .ReverseMap();
        }
    }
}
