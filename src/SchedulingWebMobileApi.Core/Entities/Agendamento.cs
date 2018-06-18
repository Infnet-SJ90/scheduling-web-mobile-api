using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Entities
{
    public class Agendamento
    {
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Local Endereco { get; set; }
    }
}
