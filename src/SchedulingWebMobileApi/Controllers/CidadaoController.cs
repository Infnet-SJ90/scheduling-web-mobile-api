using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingWebMobileApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/cidadao")]
    public class CidadaoController : Controller
    {
        private readonly ICidadaoAppService _cidadaoAppService;
        private readonly IMapperAdapter _mapperAdapter;

        public CidadaoController(ICidadaoAppService cidadaoAppService, IMapperAdapter mapperAdapter)
        {
            this._cidadaoAppService = cidadaoAppService;
            _mapperAdapter = mapperAdapter;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("/authentication")]
        public IActionResult Authentication([FromBody]AuthenticationRequestModel authentication)
        {
            if (authentication.IsValid())
            {
                var response = _cidadaoAppService.Authentication(authentication);
            }

            var badRequest = new BadRequestResponse("The fields E-mail/Cpf and Senha are required");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
