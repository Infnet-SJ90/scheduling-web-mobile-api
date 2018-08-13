using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Exceptions
{
    public class ForbbidenException : Exception
    {
        public ForbbidenException(string message) : base(message) { }
    }
}
