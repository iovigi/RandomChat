namespace RandomChat.Client.Services.Contracts
{
    using System;

    public interface IServerManager
    {
        bool IsConnect { get; }
        string Id { get; }

        event Action Connected;
        event Action Disconnected;
        event Action LostConnection;

        void Connect();
        void Disconnect();
    }
}
