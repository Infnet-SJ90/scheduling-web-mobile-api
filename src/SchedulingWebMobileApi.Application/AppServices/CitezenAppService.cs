using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Citezen;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class CitezenAppService : ICitezenAppService
    {
        private readonly ICitezenService _citezenService;
        private readonly IMapperAdapter _mapperAdapter;

        public CitezenAppService(ICitezenService citezenService, IMapperAdapter mapperAdapter)
        {
            this._citezenService = citezenService;
            _mapperAdapter = mapperAdapter;
        }

        public IResponse Delete(Guid key)
        {
            try
            {
                _citezenService.Delete(key);
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
                var citezen = _citezenService.Get(key);
                return _mapperAdapter.Map<Citezen, CitezenOkResponseModel>(citezen);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResponseModel(ex.Message);
            }
        }

        public IResponse Insert(CitezenRequestModel entity)
        {
            try
            {
                var citezen = _mapperAdapter.Map<CitezenRequestModel, Citezen>(entity);
                citezen = _citezenService.Insert(citezen);
                return _mapperAdapter.Map<Citezen, CitezenOkResponseModel>(citezen);
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

        public IResponse Update(CitezenRequestModel entity)
        {
            try
            {
                var citezen = _mapperAdapter.Map<CitezenRequestModel, Citezen>(entity);
                citezen = _citezenService.Update(citezen);
                return _mapperAdapter.Map<Citezen, CitezenOkResponseModel>(citezen);
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
