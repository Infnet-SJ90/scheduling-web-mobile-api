using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Request
{
public class AgendamentoRequestModel
    {
        public Guid AgendamentoKey { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:m:s}", ApplyFormatInEditMode = true)]
        public DateTime Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Guid EnderecoKey { get; set; }
    }
}
