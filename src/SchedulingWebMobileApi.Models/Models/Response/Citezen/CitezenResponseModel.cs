using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Citezen
{
    public class CitezenResponseModel
    {
        public Guid CitezenKey { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
