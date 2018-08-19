using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message) { }
    }
}
