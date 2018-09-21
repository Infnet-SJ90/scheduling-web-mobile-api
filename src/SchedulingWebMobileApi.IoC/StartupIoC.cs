using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SchedulingWebMobileApi.Application.AppServices;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
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
            services.AddSingleton<IMapperAdapter, MapperAdapter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            RegisterRepository(services, configuration);
            RegisterApplicationServices(services);
            RegisterDomainServices(services);
        }

        private static void RegisterRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnection>(new MySqlConnection(configuration.GetConnectionString("Database")));
            services.AddTransient<ICitezenRepository, CitezenRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<ISchedulingRepository, SchedulingRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<ICitezenAppService, CitezenAppService>();
            services.AddTransient<IAuthAppService, AuthAppService>();
            services.AddTransient<ISchedulingAppService, SchedulingAppService>();
            services.AddTransient<IAddressAppService, AddressAppService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient<ICitezenService, CitezenService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ISchedulingService, SchedulingService>();
            services.AddTransient<IAddressService, AddressService>();
        }
    }
}
