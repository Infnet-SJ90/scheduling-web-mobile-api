using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Application.Interfaces
{
    public interface ISchedulingAppService : IAppServiceBase<SchedulingRequestModel, IResponse>
    {
        IResponse GetAll();
    }
}
