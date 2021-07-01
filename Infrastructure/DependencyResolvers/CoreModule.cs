using Infrastructure.CrossCuttingConcerns.Caching;
using Infrastructure.CrossCuttingConcerns.Caching.Microsoft;
using Infrastructure.Helpers.ElasticSearch;
using Infrastructure.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();

            Log.Logger = new LoggerConfiguration()
          .WriteTo.Elasticsearch(ElasticConfigurationHelper.Host)
          .CreateLogger();

            services.AddSingleton(Log.Logger);

        }
    }
}
