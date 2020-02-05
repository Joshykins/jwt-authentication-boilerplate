using System;
using System.Collections.Generic;
using System.Text;

namespace mul.dtos
{
    public class AuthenticatedDto
    {
        public string Token { get; set; }
        public Authorization Authorized { get; set; }
    }
    public class Authorization
    {
        public bool Owner { get; set; }
    }
}
