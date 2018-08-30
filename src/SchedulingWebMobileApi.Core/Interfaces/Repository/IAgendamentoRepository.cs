using SchedulingWebMobileApi.Domain;
using System;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface IAgendamentoRepository : IRepositoryBase<Agendamento>
    {
        bool Exists(Agendamento agendamento);
    }
}