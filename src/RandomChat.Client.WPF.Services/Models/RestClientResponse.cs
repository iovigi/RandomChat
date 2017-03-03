namespace RandomChat.Client.Services.Models
{
    using System.Net;
    using System.Collections.Generic;

    public class RestClientResponse
    {
        public RestClientResponse()
        {
            this.Headers = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Headers { get; set; }

        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
