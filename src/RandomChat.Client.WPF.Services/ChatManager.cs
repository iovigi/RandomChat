namespace RandomChat.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Newtonsoft.Json;
    using Contracts;
    using Common.Server;
    using Models;

    public class ChatManager : IChatManager
    {
        private const int TIME_OF_CHECH_CHAT = 1 * 1000;//1 second

        private readonly IServerManager serverManager;
        private readonly IRestClient restClient;

        private Timer chatChecker;

        public ChatManager(IServerManager serverManager, IRestClient restClient)
        {
            this.serverManager = serverManager;
            this.restClient = restClient;
        }

        public bool IsInChat { get; private set; }

        public event Action JoinedChat;
        public event Action LeftChat;
        public event Action<Message> NewMessage;

        public void AddMessage(string content)
        {
            if (this.serverManager.IsConnect && this.IsInChat)
            {
                this.restClient.Post(ServiceConstants.ADD_MESSAGE_ADDRESS, this.GetHeaders(), null, new Message(DateTime.Now, content));
            }
        }

        public void FindChat()
        {
            if (this.serverManager.IsConnect)
            {
                this.restClient.Post(ServiceConstants.FIND_FREE_CLIENT_TO_CHAT_ADDRESS, this.GetHeaders());

                this.chatChecker = new Timer(CheckChat, null, TIME_OF_CHECH_CHAT, TIME_OF_CHECH_CHAT);
            }
        }

        public void LeaveChat()
        {
            this.SetAsLeft();

            if (this.serverManager.IsConnect)
            {
                this.restClient.Post(ServiceConstants.LEAVE_CHAT_ADDRESS, this.GetHeaders());
            }
        }

        private void SetAsLeft()
        {
            this.chatChecker?.Dispose();
            this.chatChecker = null;

            if (this.IsInChat)
            {
                this.IsInChat = false;
                this.LeftChat?.Invoke();
            }
        }

        private void CheckChat(object state)
        {
            if (!this.serverManager.IsConnect)
            {
                this.SetAsLeft();

                return;
            }

            var isInChat = bool.Parse(this.restClient.Post(ServiceConstants.IS_IN_CHAT_ADDRESS, this.GetHeaders()).Content);

            if (!this.IsInChat && isInChat)
            {
                this.IsInChat = true;
                this.JoinedChat?.Invoke();
            }
            else if (!this.IsInChat && !isInChat)
            {
                return;
            }
            else if (!isInChat)
            {
                this.SetAsLeft();

                return;
            }

            this.ReadNewMessages();
        }

        private void ReadNewMessages()
        {
            var result = this.restClient.Post(ServiceConstants.GET_MESSAGE_FROM_OTHER_CLIENT_AFTER_ADDRESS, this.GetHeaders(), null, DateTime.Now);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return;
            }

            var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(result.Content);

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    this.NewMessage?.Invoke(message);
                }
            }
        }

        private Dictionary<string, string> GetHeaders()
        {
            var headers = new Dictionary<string, string>();
            headers.Add(ServiceHeaderConstants.ID, this.serverManager.Id);

            return headers;
        }
    }
}
