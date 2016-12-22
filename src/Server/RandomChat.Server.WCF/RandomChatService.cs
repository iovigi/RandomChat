namespace RandomChat.Server.WCF
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using Core;

    public class RandomChatService : IRandomChatService
    {
        private readonly IChatServiceManager manager;

        public RandomChatService(IChatServiceManager manager)
        {
            if(manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            this.manager = manager;
        }

        public bool AddMessage(string message, DateTime sendOn)
        {
            return this.manager.AddMessage(this.GetIP(), message, sendOn);
        }

        public void FindFreeClientToChat()
        {
            this.manager.FindFreeClientToChat(this.GetIP());
        }

        public IEnumerable<DataContracts.Message> GetMessageFromOtherClientAfter(DateTime date)
        {
            return this.manager.GetMessageFromOtherClientAfter(date, this.GetIP());
        }

        public bool IsInChat()
        {
            return this.manager.IsInChat(this.GetIP());
        }

        public bool JoinToServer()
        {
            return this.manager.JoinToServer(this.GetIP());
        }

        public void LeaveChat()
        {
            this.manager.LeaveChat(this.GetIP());
        }

        public void LeaveServer()
        {
            this.manager.LeaveServer(this.GetIP());
        }

        public void Ping()
        {
            this.manager.Ping(this.GetIP());
        }

        private string GetIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            return ip;
        }
    }
}
