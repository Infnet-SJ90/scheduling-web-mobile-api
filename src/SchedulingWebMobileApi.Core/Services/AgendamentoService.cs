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
        private readonly ILocalRepository _localRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, ILocalRepository localRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _localRepository = localRepository;
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

                var address = _localRepository.Get(entity.Endereco.EnderecoKey);

                if (address == null)
                    throw new NotFoundException("Address not found");

                var hasScheduling = _agendamentoRepository.Exists(entity);

                if (!hasScheduling)
                {
                    entity.Endereco = address;
                    return _agendamentoRepository.Insert(entity);
                }

                throw new ForbbidenException("Scheduling already exists");
            }
            catch(ForbbidenException ex)
            {
                throw new ForbbidenException($"Not was possible insert the Scheduling: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException($"Not was possible insert the Scheduling: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible insert the Scheduling: {ex.Message}");
            }
        }

        public Agendamento Update(Agendamento entity)
        {
            try
            {
                var agendamento = Get(entity.AgendamentoKey);

                if ((agendamento.Data != entity.Data || agendamento.Hora != entity.Hora) && _agendamentoRepository.Exists(entity))
                    throw new ForbbidenException("Scheduling already exists");

                if (agendamento.Endereco.EnderecoKey != entity.Endereco.EnderecoKey)
                {
                    var address = _localRepository.Get(entity.Endereco.EnderecoKey);
                    entity.Endereco = address ?? throw new NotFoundException("New Address not found");
                }

                return _agendamentoRepository.Update(entity);
            }
            catch (ForbbidenException ex)
            {
                throw new ForbbidenException($"Not was possible update the Scheduling: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException($"Not was possible update the Scheduling: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible update the Scheduling: {ex.Message}");
            }
        }
    }
}
