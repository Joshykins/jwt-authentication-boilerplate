using System;
using System.Collections.Generic;
using System.Text;

namespace mul.dtos
{
    public class ErrorDto
    {
        public bool Errored { get; set; } = false;
        public List<string> ErrorMessages { get; set; } = new List<string> { };
    }
}
