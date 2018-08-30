using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Context;
using SchedulingWebMobileApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SchedulingWebMobileApi.Core.Repository
{
    public abstract class RepositoryBase<T> : BaseResource, IRepositoryBase<T> where T : class
    {
        protected readonly IDbConnection _connection;

        public RepositoryBase(IDbConnection connection, IHttpContextAccessor context) : base(context)
        {
            this._connection = connection;
        }

        public abstract T Get(Guid key);
        public abstract T Insert(T entity);
        public abstract T Update(T entity);
        public abstract bool Delete(Guid key);
    }
}
