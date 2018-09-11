using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Services
{
    public class LocalService : ILocalService
    {
        private readonly ILocalRepository _localRepository;

        public LocalService(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Address not found");

            try
            {
                return _localRepository.Delete(key);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible delete the Address: {ex.Message}");
            }
        }

        public Local Get(Guid key)
        {
            var local = _localRepository.Get(key);

            if (local == null)
                throw new NotFoundException("Address not found");

            return local;
        }

        public IList<Local> Get()
        {
            var locais = _localRepository.Get();

            if (locais == null)
                throw new NotFoundException("Addresses not found");

            return locais;

        }

        public Local Insert(Local entity)
        {
            try
            {
                entity.EnderecoKey = Guid.NewGuid();

                var hasAddress = _localRepository.Exists(entity);

                if (!hasAddress)
                    return _localRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible insert the Address: {ex.Message}");
            }

            throw new ForbbidenException("Address already exists");
        }

        public Local Update(Local entity)
        {
            throw new NotImplementedException();
        }
    }
}
