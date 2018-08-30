using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Cidadao;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;

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

        public IResponse Delete(Guid key)
        {
            try
            {
                _cidadaoService.Delete(key);
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
                var cidadao = _cidadaoService.Get(key);
                return _mapperAdapter.Map<Cidadao, CidadaoOkResponseModel>(cidadao);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
        }

        public IResponse Insert(CidadaoRequestModel entity)
        {
            try
            {
                var cidadao = _mapperAdapter.Map<CidadaoRequestModel, Cidadao>(entity);
                cidadao = _cidadaoService.Insert(cidadao);
                return _mapperAdapter.Map<Cidadao, CidadaoOkResponseModel>(cidadao);
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

        public IResponse Update(CidadaoRequestModel entity)
        {
            try
            {
                var cidadao = _mapperAdapter.Map<CidadaoRequestModel, Cidadao>(entity);
                cidadao = _cidadaoService.Update(cidadao);
                return _mapperAdapter.Map<Cidadao, CidadaoOkResponseModel>(cidadao);
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
