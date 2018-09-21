using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Domain
{
    public class Scheduling
    {
        public Guid SchedulingKey { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }
        public Citezen Citezen { get; set; }
    }
}
