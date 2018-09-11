using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface ILocalRepository : IRepositoryBase<Local>
    {
        IList<Local> Get();
        bool Exists(Local agendamento);
    }
}