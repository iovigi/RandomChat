namespace RandomChat.Server.WCF.Conversation
{
    using System.Collections.Generic;
    using DataContracts;

    public class Conversation
    {
        public Conversation(Client firstClient,Client secondClient)
        {
            this.FirstClient = firstClient;
            this.SecondClient = secondClient;
            this.Messages = new List<Message>();
        }

        public Client FirstClient { get; set; }

        public Client SecondClient { get; set; }

        public List<Message> Messages { get; set; }
    }
}
