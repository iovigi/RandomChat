﻿namespace RandomChat.Server.WCF.Core
{
    using System;
    using DataContracts; 

    public interface IClientManager
    {
        bool IsShutDown { get; }

        event Action<Client> ClientLeave;

        bool JoinClient(Client client);

        bool LeaveClient(Client client);

        Client GetFreeClient();

        void SetNotFreeClient(Client client);

        void SetFreeClient(Client client);

        Client GetClient(string id);

        void Ping(Client client);

        bool IsClientFree(Client client);

        void Start();
        void Stop();
    }
}
