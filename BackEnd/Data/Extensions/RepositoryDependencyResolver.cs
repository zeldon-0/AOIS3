using Core.Repositories.Contracts;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Extensions
{
    public static class RepositoryDependencyResolver
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDrugRepository, DrugRepository>();
            services.AddScoped<ISuffixRepository, SuffixRepository>();

        }
    }
}
