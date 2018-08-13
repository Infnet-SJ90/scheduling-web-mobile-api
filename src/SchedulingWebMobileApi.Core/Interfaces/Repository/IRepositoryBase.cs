using System;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface IRepositoryBase<T>
    {
        T Get(Guid key);
        T Insert(T entity);
        T Update(T entity);
        bool Delete(Guid key);
    }
}