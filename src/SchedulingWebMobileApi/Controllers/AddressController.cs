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
    [Route("v1/address")]
    public class AddressController : Controller
    {
        private readonly IAddressAppService _addressAppService;

        public AddressController(IAddressAppService addressAppService)
        {
            _addressAppService = addressAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = this._addressAppService.GetAll();
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._addressAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]AddressRequestModel address)
        {
            if (ModelState.IsValid)
            {
                var response = this._addressAppService.Insert(address);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var badRequest = new BadRequestResponse($"{ModelState.Keys.FirstOrDefault()} obrigatório");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]AddressRequestModel address)
        {
            var response = new UnauthorizedResponseModel("you don't have permission.");
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._addressAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }
    }
}
