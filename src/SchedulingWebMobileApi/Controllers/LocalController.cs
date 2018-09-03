using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Common;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulingWebMobileApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/local")]
    public class LocalController : Controller
    {
        private readonly ILocalAppService _localAppService;

        public LocalController(ILocalAppService localAppService)
        {
            _localAppService = localAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = this._localAppService.GetAll();
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._localAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]LocalRequestModel local)
        {
            if (ModelState.IsValid)
            {
                var response = this._localAppService.Insert(local);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var badRequest = new BadRequestResponse($"{ModelState.Keys.FirstOrDefault()} obrigatório");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]LocalRequestModel local)
        {
            var response = new UnauthorizedResponseModel("you don't have permission.");
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._localAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }
    }
}
