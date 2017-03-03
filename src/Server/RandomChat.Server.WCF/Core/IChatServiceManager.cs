namespace RandomChat.Server.WCF.Core
{
    using System;
    using System.Collections.Generic;
    using DataContracts;

    public interface IChatServiceManager
    {
        bool IsShutDown { get; }


        string JoinToServer();

        void LeaveServer(string id);

        void Ping(string id);

        void FindFreeClientToChat(string id);

        void LeaveChat(string id);

        bool IsInChat(string id);

        bool AddMessage(string id, string message, DateTime sendOn);

        IEnumerable<Message> GetMessageFromOtherClientAfter(DateTime date, string ip);
        void Start();
        void Stop();
    }
}
