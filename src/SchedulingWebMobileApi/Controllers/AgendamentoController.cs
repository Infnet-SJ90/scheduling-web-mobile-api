using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingWebMobileApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/agendamento")]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoAppService _agendamentoAppService;

        public AgendamentoController(IAgendamentoAppService agendamentoAppService)
        {
            _agendamentoAppService = agendamentoAppService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._agendamentoAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]AgendamentoRequestModel agendamento)
        {
            if (ModelState.IsValid)
            {
                var response = this._agendamentoAppService.Insert(agendamento);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var badRequest = new BadRequestResponse($"{ModelState.Keys.FirstOrDefault()} obrigatório");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]AgendamentoRequestModel agendamento)
        {
            var response = this._agendamentoAppService.Update(agendamento);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._agendamentoAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }
    }
}
