using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingWebMobileApi.Models.Request
{
    public class AuthenticationRequestModel
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public Guid Token { get; set; }
        public int AuthenticationType { get; set; }

        public bool IsValid()
        {
            switch (AuthenticationType)
            {
                case 0:
                    return Token != Guid.Empty;
                case 1:
                    return !string.IsNullOrEmpty(Senha) && (!string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Cpf));
                default:
                    return false;
            }
        }
    }
}
