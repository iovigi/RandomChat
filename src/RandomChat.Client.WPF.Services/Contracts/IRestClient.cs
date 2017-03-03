namespace RandomChat.Client.Services.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IRestClient
    {
        RestClientResponse Post(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null);

        RestClientResponse Get(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null);

        RestClientResponse Put(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null);

        RestClientResponse Delete(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null);
    }
}
