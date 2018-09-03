using SchedulingWebMobileApi.Domain;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface IAgendamentoService : IServiceBase<Agendamento>
    {
        IList<Agendamento> Get();
    }
}
