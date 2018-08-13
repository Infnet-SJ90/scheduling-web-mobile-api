using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using System;

namespace SchedulingWebMobileApi.Core.Services
{
    public class CidadaoService : ICidadaoService
    {
        private readonly ICidadaoRepository _cidadaoRepository;

        public CidadaoService(ICidadaoRepository cidadaoRepository)
        {
            _cidadaoRepository = cidadaoRepository;
        }

        public Guid Authentication(Authentication cidadao)
        {
            try
            {
                return _cidadaoRepository.Authentication(cidadao);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not possible authenticate the cidadao");
            }
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Citizen not found");

            try
            {
                return _cidadaoRepository.Delete(key);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not possible delete the citizen");
            }
        }

        public Cidadao Get(Guid key)
        {
            var cidadao = _cidadaoRepository.Get(key);

            if (cidadao == null)
                throw new NotFoundException("Citizen not found");

            return cidadao;
        }

        public Cidadao Insert(Cidadao entity)
        {
            try
            {
                entity.CidadaoKey = Guid.NewGuid();

                var hasCidadao = _cidadaoRepository.Exists(entity);

                if (!hasCidadao)
                    return _cidadaoRepository.Insert(entity);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not was possible insert the cidadao");
            }

            throw new ForbbidenException("Cidadao already exists");
        }

        public Cidadao Update(Cidadao entity)
        {
            var cidadao = Get(entity.CidadaoKey);

            if (cidadao.Email != entity.Email && _cidadaoRepository.Exists(entity.Email))
                throw new ForbbidenException("Email already exists");

            if(cidadao.Cpf != entity.Cpf)
                throw new ForbbidenException("CPF can't be updated");

            try
            {
                return _cidadaoRepository.Update(entity);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not was possible update the user");
            }
        }
    }
}
