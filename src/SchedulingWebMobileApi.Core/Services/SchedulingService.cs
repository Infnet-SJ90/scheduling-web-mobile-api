using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingRepository _schedulingRepository;
        private readonly IAddressRepository _addressRepository;

        public SchedulingService(ISchedulingRepository schedulingRepository, IAddressRepository addressRepository)
        {
            _schedulingRepository = schedulingRepository;
            _addressRepository = addressRepository;
        }

        public bool Delete(Guid key)
        {
            if (Get(key) == null)
                throw new NotFoundException("Scheduling not found");

            try
            {
                return _schedulingRepository.Delete(key);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not was possible delete the Scheduling: {ex.Message}");
            }
        }

        public Scheduling Get(Guid key)
        {
            var scheduling = _schedulingRepository.Get(key);

            if (scheduling == null)
                throw new NotFoundException("Scheduling not found");

            return scheduling;
        }

        public IList<Scheduling> Get()
        {
            var schedulings = _schedulingRepository.Get();

            if (schedulings == null)
                throw new NotFoundException("Schedules not found");

            return schedulings;
        }

        public Scheduling Insert(Scheduling entity)
        {
            try
            {
                entity.SchedulingKey = Guid.NewGuid();

                var address = _addressRepository.Get(entity.Address.AddressKey);

                if (address == null)
                    throw new NotFoundException("Address not found");

                var hasScheduling = _schedulingRepository.Exists(entity);

                if (!hasScheduling)
                {
                    entity.Address = address;
                    return _schedulingRepository.Insert(entity);
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

        public Scheduling Update(Scheduling entity)
        {
            try
            {
                var scheduling = Get(entity.SchedulingKey);

                if ((scheduling.Data != entity.Data || scheduling.Hora != entity.Hora) && _schedulingRepository.Exists(entity))
                    throw new ForbbidenException("Scheduling already exists");

                if (scheduling.Address.AddressKey != entity.Address.AddressKey)
                {
                    var address = _addressRepository.Get(entity.Address.AddressKey);
                    entity.Address = address ?? throw new NotFoundException("New Address not found");
                }

                return _schedulingRepository.Update(entity);
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
