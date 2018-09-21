using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Citezen
    {
        public Guid CitezenKey { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public List<Scheduling> Schedulings { get; set; }

        public Citezen(){}
        public Citezen(string email, string cpf, string senha)
        {
            Email = email;
            Cpf = cpf;
            Senha = senha;
        }

    }
}
