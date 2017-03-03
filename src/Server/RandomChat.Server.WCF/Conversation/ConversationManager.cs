namespace RandomChat.Server.WCF.Conversation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataContracts;

    public class ConversationManager : IConversationManager
    {
        private readonly List<Conversation> conversations;

        public ConversationManager()
        {
            this.conversations = new List<Conversation>();
        }

        public event Action<Client, Client> ConversationEnd;
        public event Action<Client, Client> ConversationStart;

        public bool AddMessage(Message message)
        {
            if(message == null)
            {
                throw new ArgumentNullException("message");
            }

            var conversation = this.conversations.FirstOrDefault(x => x.FirstClient.ID == message.ID || x.SecondClient.ID == message.ID);

            if(conversation == null)
            {
                return false;
            }

            conversation.Messages.Add(message);

            return true;
        }

        public bool EndConversation(Client client)
        {
            var conversation = GetConversation(client);

            if(conversation == null)
            {
                return false;
            }

            this.conversations.Remove(conversation);

            this.OnConversationEnd(conversation.FirstClient, conversation.SecondClient);

            return true;
        }

        public Conversation GetConversation(Client client)
        {
           return this.conversations.FirstOrDefault(x => x.FirstClient == client || x.SecondClient == client);
        }

        public IEnumerable<Message> GetMessagesFromConversationAfter(DateTime date, Client client)
        {
            var conversation = this.GetConversation(client);

            if(conversation == null)
            {
                return null;
            }

            return conversation.Messages.Where(x => x.SendOn > date);
        }

        public Conversation StartConversation(Client firstClient, Client secondClient)
        {
            if (this.conversations.Any(x => x.FirstClient == firstClient || x.SecondClient == secondClient))
            {
                return null;
            }

            var conversation = new Conversation(firstClient, secondClient);
            this.conversations.Add(conversation);

            this.OnConversationStart(firstClient, secondClient);

            return conversation;
        }

        protected void OnConversationStart(Client firstClient, Client secondClient)
        {
            ConversationStart?.Invoke(firstClient, secondClient);
        }

        protected void OnConversationEnd(Client firstClient, Client secondClient)
        {
            ConversationEnd?.Invoke(firstClient, secondClient);
        }
    }
}
