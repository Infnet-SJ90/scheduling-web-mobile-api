using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Agendamento
    {
        public Guid AgendamentoKey { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Local Endereco { get; set; }
        public Cidadao Cidadao { get; set; }
    }
}
