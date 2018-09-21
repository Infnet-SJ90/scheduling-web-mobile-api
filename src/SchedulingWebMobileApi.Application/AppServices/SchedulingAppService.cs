using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Scheduling;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class SchedulingAppService : BaseResource, ISchedulingAppService
    {
        private readonly ISchedulingService _schedulingService;
        private readonly IMapperAdapter _mapperAdapter;
        private readonly IAuthAppService _authAppService;

        public SchedulingAppService(IHttpContextAccessor context, ISchedulingService schedulingService, IAuthAppService authAppService, IMapperAdapter mapperAdapter) : base(context)
        {
            _schedulingService = schedulingService;
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

                _schedulingService.Delete(key);
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

                var scheduling = _schedulingService.Get(key);
                return _mapperAdapter.Map<Scheduling, SchedulingOkResponseModel>(scheduling);
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

                var schedulings = _schedulingService.Get();
                var schedulingsResponse = _mapperAdapter.Map<IList<Scheduling>, IList<SchedulingResponseModel>>(schedulings);
                return _mapperAdapter.Map<IList<SchedulingResponseModel>, SchedulingsOkResponseModel>(schedulingsResponse);
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

        public IResponse Insert(SchedulingRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var scheduling = _mapperAdapter.Map<SchedulingRequestModel, Scheduling>(entity);
                scheduling = _schedulingService.Insert(scheduling);
                return _mapperAdapter.Map<Scheduling, SchedulingOkResponseModel>(scheduling);
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

        public IResponse Update(SchedulingRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var scheduling = _mapperAdapter.Map<SchedulingRequestModel, Scheduling>(entity);
                scheduling = _schedulingService.Update(scheduling);
                return _mapperAdapter.Map<Scheduling, SchedulingOkResponseModel>(scheduling);
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
