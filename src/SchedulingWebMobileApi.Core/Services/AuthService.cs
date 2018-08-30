using SchedulingWebMobileApi.Core.Exceptions;
using SchedulingWebMobileApi.Core.Interfaces;
using SchedulingWebMobileApi.Core.Interfaces.Services;
using SchedulingWebMobileApi.Domain;
using System;

namespace SchedulingWebMobileApi.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Guid Authentication(Authentication auth)
        {
            try
            {
                return _authRepository.Authentication(auth);
            }
            catch (Exception)
            {
                throw new InternalServerErrorException("Not possible authenticate");
            }
        }

        public bool IsTokenValid(Guid token)
        {
            try
            {
                return _authRepository.IsTokenValid(token);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException($"Not possible authenticate: {ex.Message}");
            }
        }
    }
}
