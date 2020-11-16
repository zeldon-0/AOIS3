using Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DataDependencyResolver
    {
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddRepositories();
        }
    }
}
