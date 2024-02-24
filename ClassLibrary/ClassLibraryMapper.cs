using AutoMapper;
using ClassLibrary.Entities;
using ClassLibraryContracts.Models;

namespace ClassLibrary
{
    public class ClassLibraryMapper : Profile
    {
        public ClassLibraryMapper()
        {
            CreateMap<Customer, CustomerDto>()                
                .ReverseMap();
        }
    }
}
