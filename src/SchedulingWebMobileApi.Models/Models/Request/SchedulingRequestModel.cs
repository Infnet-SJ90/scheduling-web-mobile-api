using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SchedulingWebMobileApi.Models.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Request
{
    public class SchedulingRequestModel
    {
        public Guid SchedulingKey { get; set; }
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:m:s}", ApplyFormatInEditMode = true)]
        public DateTime Hora { get; set; }
        [Required(ErrorMessage = "O Tipo é obrigatório")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "O Status é obrigatório")]
        public string Status { get; set; }
        [Required(ErrorMessage = "O AddressKey é obrigatório")]
        public Guid AddressKey { get; set; }

        public bool IsValid(ModelStateDictionary models)
        {
            if (this.Data == DateTime.MinValue)
                models.AddModelError("Data", "Data obrigatória");

            if (this.Data < DateTime.Now.Date)
                models.AddModelError("Data", "A Data deve ser superior a atual");

            if (this.Hora == DateTime.MinValue)
                models.AddModelError("Hora", "Hora obrigatório");

            if (this.Data == DateTime.Now.Date && this.Hora.Hour < DateTime.Now.Hour)
                models.AddModelError("Hora", "A Hora deve ser superior a atual");

            return models.IsValid;
        }
    }
}
