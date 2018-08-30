using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;

namespace SchedulingWebMobileApi.Application.Interfaces
{
    public interface IAuthAppService
    {
        IResponse Authentication(AuthenticationRequestModel authentication);
        bool IsTokenValid(Guid token);
    }
}
