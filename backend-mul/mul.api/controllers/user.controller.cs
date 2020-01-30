using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mul.dtos;

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
            else
            {
                //Register in service
            }
            return Ok("test");
        }
    }
}