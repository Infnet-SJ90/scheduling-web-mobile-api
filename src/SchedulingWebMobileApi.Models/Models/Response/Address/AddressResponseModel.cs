using SchedulingWebMobileApi.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Address
{
    public class AddressResponseModel
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
