using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Helpers
{
    public class ServiceException : Exception
    {
        public int HttpStatusCode { get; private set; }

        public ServiceException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public ServiceException(string message, int httpStatusCode, Exception innerException) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        public static ServiceException BadRequest(string message)
        {
            return new ServiceException(message, 400);
        }

        public static ServiceException Unauthorized(string message)
        {
            return new ServiceException(message, 401);
        }

        public static ServiceException NotFound(string message)
        {
            return new ServiceException(message, 404);
        }
    }
}