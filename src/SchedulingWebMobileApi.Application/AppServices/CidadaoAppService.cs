using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class CidadaoAppService : ICidadaoAppService
    {
        private readonly ICidadaoService _cidadaoService;
        private readonly IMapperAdapter _mapperAdapter;

        public CidadaoAppService(ICidadaoService cidadaoService, IMapperAdapter mapperAdapter)
        {
            this._cidadaoService = cidadaoService;
            _mapperAdapter = mapperAdapter;
        }

        public IResponse Authentication(AuthenticationRequestModel authentication)
        {
            var auth = _mapperAdapter.Map<AuthenticationRequestModel, Authentication>(authentication);
            var token = _cidadaoService.Authentication(auth);

            if (token == Guid.Empty)
                return new AuthenticationOkResponseModel("Email/Cpf or Senha invalid.");

            return _mapperAdapter.Map<Guid, AuthenticationOkResponseModel>(token);
        }

        public IResponse Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public IResponse Get(Guid key)
        {
            throw new NotImplementedException();
        }

        public IResponse Insert(CidadaoRequestModel entity)
        {
            throw new NotImplementedException();
        }

        public IResponse Update(CidadaoRequestModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
