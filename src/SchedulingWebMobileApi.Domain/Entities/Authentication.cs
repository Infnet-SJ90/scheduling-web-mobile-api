using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Authentication
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public Guid Token { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
    }

    public enum AuthenticationType
    {
        LOGOUT = 0,
        LOGIN = 1
    }
}
