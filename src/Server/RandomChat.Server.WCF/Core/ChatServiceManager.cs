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

        public bool AddMessage(string ip, string message, DateTime sendOn)
        {
            return this.conversationManager.AddMessage(new Message()
            {
                SendOn = sendOn,
                IP = ip,
                Content = message
            });
        }

        public void FindFreeClientToChat(string ip)
        {
            Task.Factory.StartNew(FindFreeClientForOtherClient, (object)ip);
        }

        public IEnumerable<Message> GetMessageFromOtherClientAfter(DateTime date, string ip)
        {
            return this.conversationManager.GetMessagesFromConversationAfter(date, this.clientManager.GetClient(ip));
        }

        public bool IsInChat(string ip)
        {
            var client = this.clientManager.GetClient(ip);

            if(client == null)
            {
                return false;
            }

            return this.clientManager.IsClientFree(client);
        }

        public bool JoinToServer(string ip)
        {
            return this.clientManager.JoinClient(new Client(ip));
        }

        public void LeaveChat(string ip)
        {
            var client = this.clientManager.GetClient(ip);

            if(client != null)
            {
                this.conversationManager.EndConversation(client);
            }
        }

        public void LeaveServer(string ip)
        {
            this.LeaveChat(ip);

            this.clientManager.LeaveClient(new Client(ip));
        }

        public void Ping(string ip)
        {
            this.clientManager.Ping(this.clientManager.GetClient(ip));
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
            var ip = (string)state;

            while(!IsShutDown)
            {
                var firstClient = this.clientManager.GetClient(ip);

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
