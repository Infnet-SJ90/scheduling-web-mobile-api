using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Agendamento;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class AgendamentoAppService : BaseResource, IAgendamentoAppService
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IMapperAdapter _mapperAdapter;
        private readonly IAuthAppService _authAppService;

        public AgendamentoAppService(IHttpContextAccessor context, IAgendamentoService agendamentoService, IAuthAppService authAppService, IMapperAdapter mapperAdapter) : base(context)
        {
            _agendamentoService = agendamentoService;
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

                _agendamentoService.Delete(key);
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

                var agendamento = _agendamentoService.Get(key);
                return _mapperAdapter.Map<Agendamento, AgendamentoOkResponseModel>(agendamento);
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

                var agendamentos = _agendamentoService.Get();
                var agendamentosResponse = _mapperAdapter.Map<IList<Agendamento>, IList<AgendamentoResponseModel>>(agendamentos);
                return _mapperAdapter.Map<IList<AgendamentoResponseModel>, AgendamentosOkResponseModel>(agendamentosResponse);
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

        public IResponse Insert(AgendamentoRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var agendamento = _mapperAdapter.Map<AgendamentoRequestModel, Agendamento>(entity);
                agendamento = _agendamentoService.Insert(agendamento);
                return _mapperAdapter.Map<Agendamento, AgendamentoOkResponseModel>(agendamento);
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

        public IResponse Update(AgendamentoRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var agendamento = _mapperAdapter.Map<AgendamentoRequestModel, Agendamento>(entity);
                agendamento = _agendamentoService.Update(agendamento);
                return _mapperAdapter.Map<Agendamento, AgendamentoOkResponseModel>(agendamento);
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
