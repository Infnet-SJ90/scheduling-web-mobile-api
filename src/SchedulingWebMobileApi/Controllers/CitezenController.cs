using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingWebMobileApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/citezen")]
    public class CitezenController : Controller
    {
        private readonly ICitezenAppService _citezenAppService;

        public CitezenController(ICitezenAppService citezenAppService)
        {
            this._citezenAppService = citezenAppService;
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._citezenAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]CitezenRequestModel citezen)
        {
            if (ModelState.IsValid)
            {
                var response = this._citezenAppService.Insert(citezen);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var badRequest = new BadRequestResponse($"{ModelState.Keys.FirstOrDefault()} obrigatório");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]CitezenRequestModel citezen)
        {
            var response = this._citezenAppService.Update(citezen);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._citezenAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }


    }
}
