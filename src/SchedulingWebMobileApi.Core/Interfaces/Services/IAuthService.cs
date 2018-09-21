using SchedulingWebMobileApi.Domain;
using System;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Guid Authentication(Authentication citezen);
        bool IsTokenValid(Guid token);
    }
}
