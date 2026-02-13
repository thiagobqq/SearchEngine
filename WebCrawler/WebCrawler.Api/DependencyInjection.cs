using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Domain.Interfaces.Repositories;
using WebCrawler.Infra.Repositories;

namespace WebCrawler.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPageRepository, PageRepository>();
            return services;
        }
    }
}