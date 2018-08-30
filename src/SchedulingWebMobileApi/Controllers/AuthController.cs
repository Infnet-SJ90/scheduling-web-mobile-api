using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingWebMobileApi.Controllers
{
    [Produces("application/json")]
    [Route("v1")]
    public class AuthController : Controller
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            this._authAppService = authAppService;
        }

        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody]AuthenticationRequestModel authentication)
        {
            if (authentication.IsValid())
            {
                var response = _authAppService.Authentication(authentication);
            }

            var badRequest = new BadRequestResponse("The fields E-mail/Cpf and Senha are required");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }
    }
}
