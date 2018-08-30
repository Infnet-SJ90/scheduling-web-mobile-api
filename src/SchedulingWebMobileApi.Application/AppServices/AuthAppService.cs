using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class AuthAppService : BaseResource, IAuthAppService
    {
        private readonly IAuthService _authService;
        private readonly IMapperAdapter _mapperAdapter;

        public AuthAppService(IHttpContextAccessor context, IAuthService authService, IMapperAdapter mapperAdapter) : base(context)
        {
            _authService = authService;
            _mapperAdapter = mapperAdapter;
        }

        public IResponse Authentication(AuthenticationRequestModel authentication)
        {
            var auth = _mapperAdapter.Map<AuthenticationRequestModel, Authentication>(authentication);
            var token = _authService.Authentication(auth);

            if (token == Guid.Empty)
                return new AuthenticationOkResponseModel("Email/Cpf or Senha invalid.");

            return _mapperAdapter.Map<Guid, AuthenticationOkResponseModel>(token);
        }

        public bool IsTokenValid(Guid token)
        {
            return _authService.IsTokenValid(token);
        }
    }
}
