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
    [Route("v1/scheduling")]
    public class SchedulingController : Controller
    {
        private readonly ISchedulingAppService _schedulingAppService;

        public SchedulingController(ISchedulingAppService schedulingAppService)
        {
            _schedulingAppService = schedulingAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = this._schedulingAppService.GetAll();
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._schedulingAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]SchedulingRequestModel scheduling)
        {
            if (scheduling != null && scheduling.IsValid(ModelState))
            {
                var response = this._schedulingAppService.Insert(scheduling);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var validate = ModelState.FirstOrDefault();
            var badRequest = new BadRequestResponse($"{validate.Value.Errors.FirstOrDefault().ErrorMessage}");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]SchedulingRequestModel scheduling)
        {
            if (scheduling.IsValid(ModelState))
            {
                var response = this._schedulingAppService.Update(scheduling);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var validate = ModelState.FirstOrDefault();
            var badRequest = new BadRequestResponse($"{validate.Value.Errors.FirstOrDefault().ErrorMessage}");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._schedulingAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }
    }
}
