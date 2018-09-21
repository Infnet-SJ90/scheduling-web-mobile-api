using SchedulingWebMobileApi.Domain;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface IAddressService : IServiceBase<Address>
    {
        IList<Address> Get();
    }
}
