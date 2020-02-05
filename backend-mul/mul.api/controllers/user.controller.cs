using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mul.dtos;
using mul.service.authenticatation;
using mul.service.signup;

namespace mul.api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("signup")]
        public ActionResult RegisterAPI(RegisterDto registerRequest)
        {
            registerRequest = SanitizeRegisterDto.Sanitize(registerRequest);
            if(registerRequest.Errored)
            {
                return BadRequest(registerRequest.ErrorMessages);
            }
            //Register in service
            var RegistrationService = new Signup();
            RegistrationService.SignupAccountAndUser(registerRequest);
            if(RegistrationService.Errored)
            {
                return BadRequest(RegistrationService.ErrorMessages);

            }

            //Authenticate and retrieve token
            var authenticator = new Authenticator();
            authenticator.AuthenticateSignin(registerRequest.Password, registerRequest.Email);
            if(authenticator.Errored)
            {
                return BadRequest(RegistrationService.ErrorMessages);
            }


            return Ok(authenticator.Token);
                
       
        }
    }
}