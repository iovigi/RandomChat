namespace RandomChat.Server.WCF.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Conversation;
    using DataContracts;
    using System.Threading.Tasks;

    public class ChatServiceManager : IChatServiceManager
    {
        private const int TIME_TO_WAIT_FOR_CHECK_AGAIN_FOR_FREE_CLIENT = 1 * 1000;//1 second;

        private readonly IClientManager clientManager;
        private readonly IConversationManager conversationManager;

        public ChatServiceManager(IClientManager clientManager, IConversationManager conversationManager)
        {
            if(clientManager == null)
            {
                throw new ArgumentNullException("clientManager");
            }

            if(conversationManager == null)
            {
                throw new ArgumentNullException("conversationManager");
            }

            this.clientManager = clientManager;
            this.conversationManager = conversationManager;

            conversationManager.ConversationStart += (x, y) => 
            {
                clientManager.SetNotFreeClient(x);
                clientManager.SetNotFreeClient(y);
            };

            conversationManager.ConversationEnd += (x, y) =>
            {
                clientManager.SetFreeClient(x);
                clientManager.SetFreeClient(y);
            };
        }

        public bool IsShutDown { get; private set; }

        public bool AddMessage(string id, string message, DateTime sendOn)
        {
            return this.conversationManager.AddMessage(new Message()
            {
                SendOn = sendOn,
                ID = id,
                Content = message
            });
        }

        public void FindFreeClientToChat(string id)
        {
            Task.Factory.StartNew(FindFreeClientForOtherClient, (object)id);
        }

        public IEnumerable<Message> GetMessageFromOtherClientAfter(DateTime date, string id)
        {
            return this.conversationManager.GetMessagesFromConversationAfter(date, this.clientManager.GetClient(id));
        }

        public bool IsInChat(string id)
        {
            var client = this.clientManager.GetClient(id);

            if(client == null)
            {
                return false;
            }

            return this.clientManager.IsClientFree(client);
        }

        public string JoinToServer()
        {
            var newId = Client.GetNextId();

            while(!this.clientManager.JoinClient(new Client(newId)))
            {
                newId = Client.GetNextId();
            }

            return newId;
        }

        public void LeaveChat(string id)
        {
            var client = this.clientManager.GetClient(id);

            if(client != null)
            {
                this.conversationManager.EndConversation(client);
            }
        }

        public void LeaveServer(string id)
        {
            this.LeaveChat(id);

            this.clientManager.LeaveClient(new Client(id));
        }

        public void Ping(string id)
        {
            this.clientManager.Ping(new Client(id));
        }

        public void Start()
        {
            this.clientManager.Start();

            this.IsShutDown = false;
        }

        public void Stop()
        {
            this.clientManager.Stop();

            this.IsShutDown = true;
        }

        //TODO:Shut down logic must be implement :)
        private void FindFreeClientForOtherClient(object state)
        {
            var id = (string)state;

            while(!IsShutDown)
            {
                var firstClient = this.clientManager.GetClient(id);

                if(firstClient == null)
                {
                    return;
                }

                var secondClient = this.clientManager.GetFreeClient();

                if(secondClient == null)
                {
                    Thread.Sleep(TIME_TO_WAIT_FOR_CHECK_AGAIN_FOR_FREE_CLIENT);

                    continue;
                }

                this.conversationManager.StartConversation(firstClient, secondClient);
            }
        }
    }
}
