using ClassLibrary.Repositories;
using ClassLibraryContracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClassLibrary
{
    public static class ClassLibraryDependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            
            return services;
        }
    }
}
