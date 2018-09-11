using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Scheduling not found");

            try
            {
                return _agendamentoRepository.Delete(key);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible delete the Scheduling: {ex.Message}");
            }
        }

        public Agendamento Get(Guid key)
        {
            var agendamento = _agendamentoRepository.Get(key);

            if (agendamento == null)
                throw new NotFoundException("Scheduling not found");

            return agendamento;
        }

        public IList<Agendamento> Get()
        {
            var agendamentos = _agendamentoRepository.Get();

            if (agendamentos == null)
                throw new NotFoundException("Schedules not found");

            return agendamentos;
        }

        public Agendamento Insert(Agendamento entity)
        {
            try
            {
                entity.AgendamentoKey = Guid.NewGuid();

                var hasScheduling = _agendamentoRepository.Exists(entity);
                //VALIDAR SE O TOKEN DO ENDEREÇO EXISTE

                if (!hasScheduling)
                    return _agendamentoRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible insert the Scheduling: {ex.Message}");
            }

            throw new ForbbidenException("Scheduling already exists");
        }

        public Agendamento Update(Agendamento entity)
        {
            var agendamento = Get(entity.AgendamentoKey);

            if (agendamento.Data != entity.Data || agendamento.Hora != entity.Hora && _agendamentoRepository.Exists(entity))
                throw new ForbbidenException("Scheduling already exists");

            //VALIDAR SE O TOKEN DO ENDEREÇO É DIFERENTE EXISTE

            try
            {
                return _agendamentoRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible update the Scheduling: {ex.Message}");
            }
        }
    }
}
