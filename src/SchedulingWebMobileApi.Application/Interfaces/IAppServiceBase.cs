using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Application.Interfaces
{
    public interface IAppServiceBase<TRequest, TResponse>
    {
        TResponse Get(Guid key);
        TResponse Insert(TRequest entity);
        TResponse Update(TRequest entity);
        TResponse Delete(Guid key);
    }
}
