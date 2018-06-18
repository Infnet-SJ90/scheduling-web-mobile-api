using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Entities
{
    public class Cidadao
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
    }
}
