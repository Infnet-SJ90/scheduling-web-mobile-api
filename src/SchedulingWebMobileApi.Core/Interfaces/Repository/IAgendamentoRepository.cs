using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface IAgendamentoRepository : IRepositoryBase<Agendamento>
    {
        IList<Agendamento> Get();
        bool Exists(Agendamento agendamento);
    }
}