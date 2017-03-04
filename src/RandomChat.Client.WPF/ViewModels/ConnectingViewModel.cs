namespace RandomChat.Client.WPF.ViewModels
{
    using Prism.Regions;
    using Common.Client;
    using Services.Contracts;

    public class ConnectingViewModel : BaseViewModel
    {
        private readonly IChatManager chatManager;

        private string state;

        public ConnectingViewModel(IRegionManager regionManager, IServerManager serverManager, IChatManager chatManager)
            : base(regionManager, serverManager)
        {
            SetState();

            this.chatManager = chatManager;

            if(serverManager.IsConnect)
            {
                this.chatManager.FindChat();
            }

            this.serverManager.Connected += OnConnected;
            this.chatManager.JoinedChat += OnJoinedChat;
        }

        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.SetProperty(ref this.state, value);
            }
        }

        protected override void OnLostConnection()
        {
            SetState();
        }

        private void OnConnected()
        {
            SetState();
            this.chatManager.FindChat();
        }

        private void OnJoinedChat()
        {
            this.RequestNavigation(ViewConstants.CHAT_VIEW_NAME);
        }

        private void SetState()
        {
            this.State = this.serverManager.IsConnect ? StateConstants.SEARCHING_FOR_CHAT : StateConstants.CONNECTING;
        }

        private class StateConstants
        {
            public const string CONNECTING = "Connecting";
            public const string SEARCHING_FOR_CHAT = "Searching for chat";
        }
    }
}
