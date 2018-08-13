using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SchedulingWebMobileApi.Application.AppServices;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Repository;
using SchedulingWebMobileApi.Core.Services;
using System;
using System.Data;

namespace SchedulingWebMobileApi.IoC
{
    public class StartupIoC
    {
        public static void RegisterIoC(IServiceCollection services, IConfiguration configuration)
        {
            RegisterRepository(services, configuration);
        }

        private static void RegisterRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnection>(new MySqlConnection(configuration.GetConnectionString("Database")));
            services.AddTransient<ICidadaoRepository, CidadaoRepository>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<ICidadaoAppService, CidadaoAppService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient<ICidadaoService, CidadaoService>();
        }
    }
}
