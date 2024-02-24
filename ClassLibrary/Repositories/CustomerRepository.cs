using AutoMapper;
using ClassLibrary.Entities;
using ClassLibraryContracts.Models;
using ClassLibraryContracts.Repositories;

namespace ClassLibrary.Repositories
{
    public class CustomerRepository : Repository<Customer, Int64, CustomerDto>, ICustomerRepository
    {
        public CustomerRepository(WebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
