using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Address
    {
        public Guid AddressKey { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
    }
}
