namespace RandomChat.Server.WCF
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Web;
    using Core;
    using Common.Server;


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
            return this.manager.AddMessage(this.GetID(), message, sendOn);
        }

        public void FindFreeClientToChat()
        {
            this.manager.FindFreeClientToChat(this.GetID());
        }

        public IEnumerable<DataContracts.Message> GetMessageFromOtherClientAfter(DateTime date)
        {
            return this.manager.GetMessageFromOtherClientAfter(date, this.GetID());
        }

        public bool IsInChat()
        {
            return this.manager.IsInChat(this.GetID());
        }

        public string JoinToServer()
        {
            return this.manager.JoinToServer();
        }

        public void LeaveChat()
        {
            this.manager.LeaveChat(this.GetID());
        }

        public void LeaveServer()
        {
            this.manager.LeaveServer(this.GetID());
        }

        public void Ping()
        {
            this.manager.Ping(this.GetID());
        }

        private string GetID()
        {
            WebOperationContext context = WebOperationContext.Current;
            string id = context.IncomingRequest.Headers[ServiceHeaderConstants.ID];

            return id;
        }
    }
}
