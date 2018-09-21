using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface ISchedulingRepository : IRepositoryBase<Scheduling>
    {
        IList<Scheduling> Get();
        bool Exists(Scheduling scheduling);
    }
}