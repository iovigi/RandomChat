namespace RandomChat.Server.WCF.Conversation
{
    using System;
    using System.Collections.Generic;
    using DataContracts;

    public interface IConversationManager
    {
        event Action<Client, Client> ConversationStart;
        event Action<Client, Client> ConversationEnd;

        Conversation StartConversation(Client firstClient, Client secondClient);

        bool EndConversation(Client client);

        bool AddMessage(Message message);

        Conversation GetConversation(Client client);

        IEnumerable<Message> GetMessagesFromConversationAfter(DateTime date, Client client);
    }
}
