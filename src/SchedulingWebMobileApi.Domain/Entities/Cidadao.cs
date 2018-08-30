using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Cidadao
    {
        public Guid CidadaoKey { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public List<Agendamento> Agendamentos { get; set; }

        public Cidadao(){}
        public Cidadao(string email, string cpf, string senha)
        {
            Email = email;
            Cpf = cpf;
            Senha = senha;
        }

    }
}
