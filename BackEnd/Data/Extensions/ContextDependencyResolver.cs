using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Extensions
{
    public static class ContextDependencyResolver
    {
        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DrugContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DrugContext"),
                b => b.MigrationsAssembly("Data"));
                options.EnableDetailedErrors();
            });
        }
    }
}
