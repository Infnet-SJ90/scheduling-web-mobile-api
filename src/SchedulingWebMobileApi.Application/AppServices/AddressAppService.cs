using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Address;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Application.AppServices
{
    public class AddressAppService : BaseResource, IAddressAppService
    {
        private readonly IAddressService _addressService;
        private readonly IMapperAdapter _mapperAdapter;
        private readonly IAuthAppService _authAppService;

        public AddressAppService(IHttpContextAccessor context, IAddressService addressService, IAuthAppService authAppService, IMapperAdapter mapperAdapter) : base(context)
        {
            _addressService = addressService;
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

                _addressService.Delete(key);
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

                var address = _addressService.Get(key);
                return _mapperAdapter.Map<Address, AddressOkResponseModel>(address);
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

                var locais = _addressService.Get();
                var locaisResponse = _mapperAdapter.Map<IList<Address>, IList<AddressResponseModel>>(locais);
                return _mapperAdapter.Map<IList<AddressResponseModel>, AddressesOkResponseModel>(locaisResponse);
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

        public IResponse Insert(AddressRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var address = _mapperAdapter.Map<AddressRequestModel, Address>(entity);
                address = _addressService.Insert(address);
                return _mapperAdapter.Map<Address, AddressOkResponseModel>(address);
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

        public IResponse Update(AddressRequestModel entity)
        {
            try
            {
                var token = Context.Request.Headers["Token"];

                if (!_authAppService.IsTokenValid(Guid.Parse(token)))
                    return new UnauthorizedResponseModel("Citezen not authenticated");

                var address = _mapperAdapter.Map<AddressRequestModel, Address>(entity);
                address = _addressService.Update(address);
                return _mapperAdapter.Map<Address, AddressOkResponseModel>(address);
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
