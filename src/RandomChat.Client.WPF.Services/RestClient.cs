namespace RandomChat.Client.Services
{
    using System;
    using System.Collections.Generic;
    using RestSharp;
    using Models;

    public class RestClient : Contracts.IRestClient
    {
        private readonly string baseUrl;
        private readonly RestSharp.RestClient client;

        public RestClient(string baseUrl)
        {
            this.baseUrl = baseUrl;

            var uri = new UriBuilder(baseUrl).Uri;
            this.client = new RestSharp.RestClient(uri);
        }

        public RestClientResponse Delete(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null)
        {
            var request = this.CreateRequest(resource, headers, parameters, body);
            request.Method = Method.DELETE;
            var response = this.client.Delete(request);

            return this.ConvertResponse(response);
        }

        public RestClientResponse Get(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null)
        {
            var request = this.CreateRequest(resource, headers, parameters, body);
            request.Method = Method.GET;
            var response = this.client.Get(request);

            return this.ConvertResponse(response);
        }

        public RestClientResponse Post(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null)
        {
            var request = this.CreateRequest(resource, headers, parameters, body);
            request.Method = Method.POST;
            var response = this.client.Post(request);

            return this.ConvertResponse(response);
        }

        public RestClientResponse Put(string resource, IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null, object body = null)
        {
            var request = this.CreateRequest(resource, headers, parameters, body);
            request.Method = Method.PUT;
            var response = this.client.Put(request);

            return this.ConvertResponse(response);
        }

        private IRestRequest CreateRequest(string resource, IDictionary<string, string> headers, IDictionary<string, string> parameters, object body)
        {
            var request = new RestRequest(resource);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            if (body != null)
            {
                request.AddJsonBody(body);
            }

            return request;
        }

        private RestClientResponse ConvertResponse(IRestResponse response)
        {
            var restClientResponse = new RestClientResponse();
            restClientResponse.StatusCode = response.StatusCode;

            foreach (var header in response.Headers)
            {
                restClientResponse.Headers.Add(header.Name, header.Value.ToString());
            }

            restClientResponse.Content = response.Content;

            return restClientResponse;
        }
    }
}
