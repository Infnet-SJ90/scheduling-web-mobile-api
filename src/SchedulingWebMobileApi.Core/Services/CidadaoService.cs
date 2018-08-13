using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public Cidadao Get(Guid key)
        {
            var cidadao = _cidadaoRepository.Get(key);

            if (cidadao == null)
                throw new NotFoundException("Cidadao not found");

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
            throw new NotImplementedException();
        }
    }
}
