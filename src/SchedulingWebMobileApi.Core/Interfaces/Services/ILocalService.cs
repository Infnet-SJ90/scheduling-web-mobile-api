using SchedulingWebMobileApi.Domain;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface ILocalService : IServiceBase<Local>
    {
        IList<Local> Get();
    }
}
