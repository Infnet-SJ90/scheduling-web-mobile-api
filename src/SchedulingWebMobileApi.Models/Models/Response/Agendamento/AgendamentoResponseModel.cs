using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Response.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Agendamento
{
    public class AgendamentoResponseModel
    {
        public Guid AgendamentoKey { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public LocalResponseModel Endereco { get; set; }
    }
}
