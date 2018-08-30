using SchedulingWebMobileApi.Domain;
using System;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface IAuthRepository
    {
        Guid Authentication(Authentication cidadao);
        bool IsTokenValid(Guid token);
    }
}