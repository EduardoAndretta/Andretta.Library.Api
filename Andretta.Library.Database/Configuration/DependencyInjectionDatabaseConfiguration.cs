using Andretta.Library.Business.Interfaces.Repositories;
using Andretta.Library.Database.Migrations;
using Andretta.Library.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Andretta.Library.Database.Configuration
{
    public static class DependencyInjectionDatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
           services.AddDbContext<InformationContext>();

            // [Repositories]

            services.AddScoped<IBookRepository, BookRepository>();

           return services;
        }
    }
}
