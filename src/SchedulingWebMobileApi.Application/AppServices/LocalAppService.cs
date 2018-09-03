using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Local;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class LocalAppService : BaseResource, ILocalAppService
    {
        private readonly ILocalService _localService;
        private readonly IMapperAdapter _mapperAdapter;
        private readonly IAuthAppService _authAppService;

        public LocalAppService(IHttpContextAccessor context, ILocalService localService, IAuthAppService authAppService, IMapperAdapter mapperAdapter) : base(context)
        {
            _localService = localService;
            _mapperAdapter = mapperAdapter;
            _authAppService = authAppService;
        }

        public IResponse Delete(Guid key)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                _localService.Delete(key);
                return new AcceptResponseModel();
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return new InternoServerErrorResponseModel(ex.Message);
            }
        }

        public IResponse Get(Guid key)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var local = _localService.Get(key);
                return _mapperAdapter.Map<Local, LocalOkResponseModel>(local);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
            catch(Exception ex)
            {
                return new InternoServerErrorResponseModel(ex.Message);
            }
        }

        public IResponse GetAll()
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var locais = _localService.Get();
                var locaisResponse = _mapperAdapter.Map<IList<Local>, IList<LocalResponseModel>>(locais);
                return _mapperAdapter.Map<IList<LocalResponseModel>, LocaisOkResponseModel>(locaisResponse);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
            catch (Exception ex)
            {
                return new InternoServerErrorResponseModel(ex.Message);
            }
        }

        public IResponse Insert(LocalRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var local = _mapperAdapter.Map<LocalRequestModel, Local>(entity);
                local = _localService.Insert(local);
                return _mapperAdapter.Map<Local, LocalOkResponseModel>(local);
            }
            catch (ForbbidenException ex)
            {
                return new ForbbidenResponseModel(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return new InternoServerErrorResponseModel(ex.Message);
            }
        }

        public IResponse Update(LocalRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var local = _mapperAdapter.Map<LocalRequestModel, Local>(entity);
                local = _localService.Update(local);
                return _mapperAdapter.Map<Local, LocalOkResponseModel>(local);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
            catch (ForbbidenException ex)
            {
                return new ForbbidenResponseModel(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return new InternoServerErrorResponseModel(ex.Message);
            }
        }
    }
}
