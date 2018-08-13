using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface IServiceBase<T>
    {
        T Get(Guid key);
        T Insert(T entity);
        T Update(T entity);
        bool Delete(Guid key);
    }
}
