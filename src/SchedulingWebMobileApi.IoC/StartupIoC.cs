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
            services.AddTransient<ICidadaoRepository, CidadaoRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<ICidadaoAppService, CidadaoAppService>();
            services.AddTransient<IAuthAppService, AuthAppService>();
            services.AddTransient<IAgendamentoAppService, AgendamentoAppService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient<ICidadaoService, CidadaoService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAgendamentoService, AgendamentoService>();
        }
    }
}
