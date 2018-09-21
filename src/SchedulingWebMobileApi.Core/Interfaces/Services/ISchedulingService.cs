using SchedulingWebMobileApi.Domain;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface ISchedulingService : IServiceBase<Scheduling>
    {
        IList<Scheduling> Get();
    }
}
