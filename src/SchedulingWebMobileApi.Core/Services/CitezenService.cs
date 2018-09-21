using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;

namespace SchedulingWebMobileApi.Core.Services
{
    public class CitezenService : ICitezenService
    {
        private readonly ICitezenRepository _citezenRepository;

        public CitezenService(ICitezenRepository citezenRepository)
        {
            _citezenRepository = citezenRepository;
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Citizen not found");

            try
            {
                return _citezenRepository.Delete(key);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not possible delete the citizen");
            }
        }

        public Citezen Get(Guid key)
        {
            var citezen = _citezenRepository.Get(key);

            if (citezen == null)
                throw new NotFoundException("Citizen not found");

            return citezen;
        }

        public Citezen Insert(Citezen entity)
        {
            try
            {
                entity.CitezenKey = Guid.NewGuid();

                var hasCitezen = _citezenRepository.Exists(entity);

                if (!hasCitezen)
                    return _citezenRepository.Insert(entity);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not was possible insert the citezen");
            }

            throw new ForbbidenException("Citezen already exists");
        }

        public Citezen Update(Citezen entity)
        {
            var citezen = Get(entity.CitezenKey);

            if (citezen.Email != entity.Email && _citezenRepository.Exists(entity.Email))
                throw new ForbbidenException("Email already exists");

            if(citezen.Cpf != entity.Cpf)
                throw new ForbbidenException("CPF can't be updated");

            try
            {
                return _citezenRepository.Update(entity);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not was possible update the user");
            }
        }
    }
}
