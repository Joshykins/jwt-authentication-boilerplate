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
        [HttpPost("signup")]
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
            var authenticator = new Authenticater();
            authenticator.AuthenticateSignin(registerRequest.Password, registerRequest.Email);
            if(authenticator.Errored)
            {
                return BadRequest(authenticator.ErrorMessages);
            }

            return Ok(authenticator.Token);
        }
        [HttpPost("signin")]
        public ActionResult signin(SigninDto signinRequest)
        {
            signinRequest = SanitizeSigninDto.Sanitize(signinRequest);
            if(signinRequest.Errored)
            {
                return BadRequest(signinRequest.ErrorMessages);
            }

            //Authenticate and retrieve token
            var authenticator = new Authenticater();
            authenticator.AuthenticateSignin(signinRequest.Password, signinRequest.Email);
            if (authenticator.Errored)
            {
                return BadRequest(authenticator.ErrorMessages);
            }

            return Ok(authenticator.Token);

        }
    }
}