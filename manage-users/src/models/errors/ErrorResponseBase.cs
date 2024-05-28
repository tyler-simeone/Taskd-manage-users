using System.Net;

namespace manage_users.src.models
{
    public class ErrorResponseBase
    {
        public HttpStatusCode Status { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
    }
}