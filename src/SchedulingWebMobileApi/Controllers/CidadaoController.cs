﻿using Microsoft.AspNetCore.Mvc;
using SchedulingWebMobileApi.Application.Interfaces;
using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Core.Mapper;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var response = this._cidadaoAppService.Get(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpPost]
        public IActionResult Post([FromBody]CidadaoRequestModel cidadao)
        {
            if (ModelState.IsValid)
            {
                var response = this._cidadaoAppService.Insert(cidadao);
                return new ObjectResult(response) { StatusCode = response.StatusCode() };
            }

            var badRequest = new BadRequestResponse($"{ModelState.Keys.FirstOrDefault()} obrigatório");
            return new ObjectResult(badRequest) { StatusCode = badRequest.StatusCode() };
        }

        [HttpPut]
        public IActionResult Put([FromBody]CidadaoRequestModel cidadao)
        {
            var response = this._cidadaoAppService.Update(cidadao);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(Guid key)
        {
            var response = this._cidadaoAppService.Delete(key);
            return new ObjectResult(response) { StatusCode = response.StatusCode() };
        }


    }
}