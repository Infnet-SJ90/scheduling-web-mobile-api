using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Response.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Scheduling
{
    public class SchedulingResponseModel
    {
        public Guid SchedulingKey { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public AddressResponseModel Address { get; set; }
    }
}
