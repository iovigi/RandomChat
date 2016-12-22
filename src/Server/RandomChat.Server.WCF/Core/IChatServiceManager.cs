namespace RandomChat.Server.WCF.Core
{
    using System;
    using System.Collections.Generic;
    using DataContracts;

    public interface IChatServiceManager
    {

        bool JoinToServer(string ip);

        void LeaveServer(string ip);

        void Ping(string ip);

        void FindFreeClientToChat(string ip);

        void LeaveChat(string ip);

        bool IsInChat(string ip);

        bool AddMessage(string ip, string message, DateTime sendOn);

        IEnumerable<Message> GetMessageFromOtherClientAfter(DateTime date, string ip);
    }
}
