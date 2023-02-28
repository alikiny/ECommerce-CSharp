using System.Net;

namespace Backend.src.Helpers
{
    public sealed class ServiceException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }

        public ServiceException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public static ServiceException BadRequest(string message)
        {
            return new ServiceException(message, HttpStatusCode.BadRequest);
        }

        public static ServiceException Unauthorized(string message)
        {
            return new ServiceException(message, HttpStatusCode.Unauthorized);
        }

        public static ServiceException NotFound(string message)
        {
            return new ServiceException(message, HttpStatusCode.NotFound);
        }

        public static ServiceException NullOrEmpty(string message)
        {
            return new ServiceException(message, HttpStatusCode.NoContent);
        }

        public static ServiceException Forbidden(string message)
        {
            return new ServiceException(message, HttpStatusCode.Forbidden);
        }
    }
}