using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        IList<Address> Get();
        bool Exists(Address scheduling);
    }
}