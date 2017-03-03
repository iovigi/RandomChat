namespace RandomChat.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Net;
    using Contracts;
    using Common.Server;

    public class ServerManager : IServerManager
    {
        private const int TIME_OF_PING = 1 * 1000;

        private readonly IRestClient restClient;

        private Timer pingTimer;
        public ServerManager(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public bool IsConnect { get; private set; }
        public string Id { get; private set; }

        public event Action Connected;
        public event Action Disconnected;
        public event Action LostConnection;

        public void Connect()
        {
            this.pingTimer = new Timer(Ping, null, 0, TIME_OF_PING);
        }

        public void Disconnect()
        {
            this.pingTimer?.Dispose();
            this.pingTimer = null;

            this.restClient.Post(ServiceConstants.LEAVE_SERVER_ADDRESS, this.GetHeaders());

            this.IsConnect = false;
            this.Disconnected?.Invoke();
        }

        private void Ping(object state)
        {
            if (!this.IsConnect)
            {
                var result = this.restClient.Post(ServiceConstants.JOIN_TO_SERVER_ADDRESS);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    this.IsConnect = true;
                    this.Id = result.Content;
                    this.Connected?.Invoke();
                }
                else
                {
                    this.IsConnect = false;
                }
            }
            else
            {
                var result = this.restClient.Post(ServiceConstants.PING_ADDRESS, this.GetHeaders());
                this.IsConnect = result.StatusCode == HttpStatusCode.OK;

                if (!this.IsConnect)
                {
                    this.LostConnection?.Invoke();
                }
            }
        }

        private Dictionary<string, string> GetHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add(ServiceHeaderConstants.ID, this.Id);

            return headers;
        }
    }
}
