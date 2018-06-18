using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Entities
{
    public class Local
    {
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
    }
}
