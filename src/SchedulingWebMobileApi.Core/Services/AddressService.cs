using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Address not found");

            try
            {
                return _addressRepository.Delete(key);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible delete the Address: {ex.Message}");
            }
        }

        public Address Get(Guid key)
        {
            var address = _addressRepository.Get(key);

            if (address == null)
                throw new NotFoundException("Address not found");

            return address;
        }

        public IList<Address> Get()
        {
            var locais = _addressRepository.Get();

            if (locais == null)
                throw new NotFoundException("Addresses not found");

            return locais;

        }

        public Address Insert(Address entity)
        {
            try
            {
                entity.AddressKey = Guid.NewGuid();

                var hasAddress = _addressRepository.Exists(entity);

                if (!hasAddress)
                    return _addressRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible insert the Address: {ex.Message}");
            }

            throw new ForbbidenException("Address already exists");
        }

        public Address Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
