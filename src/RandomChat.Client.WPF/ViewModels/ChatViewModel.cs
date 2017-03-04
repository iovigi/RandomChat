namespace RandomChat.Client.WPF.ViewModels
{
    using System;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Regions;
    using Services.Models;
    using Services.Contracts;

    public class ChatViewModel : BaseViewModel
    {
        private const string YOU = "You";
        private const string STRANGER = "Stranger";

        private readonly IChatManager chatManager;

        private string message;
        private string messages;

        public ChatViewModel(IRegionManager regionManager, IServerManager serverManager, IChatManager chatManager)
            : base(regionManager, serverManager)
        {
            this.chatManager = chatManager;

            if(!this.chatManager.IsInChat)
            {
                this.GoToConnectingScreen();

                return;
            }

            this.chatManager.LeftChat += GoToConnectingScreen;
            this.chatManager.NewMessage += OnNewMessage;

            this.SendCommand = new DelegateCommand<string>(Send);
            this.LeaveCommand = new DelegateCommand(Leave);
        }

        public ICommand SendCommand { get; set; }
        public ICommand LeaveCommand { get; set; }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.SetProperty(ref this.message, value);
            }
        }

        public string Messages
        {
            get
            {
                return this.messages;
            }
            set
            {
                this.SetProperty(ref this.messages, value);
            }
        }

        private void OnNewMessage(Message message)
        {
            this.AppendMessage(STRANGER, message.Content);
        }

        private void Send(string text)
        {
            this.AppendMessage(YOU, text);
            this.chatManager.AddMessage(text);
            this.Message = string.Empty;
        }

        private void Leave()
        {
            this.chatManager.LeaveChat();
        }

        private void AppendMessage(string from, string message)
        {
            this.Message += string.Format("{0}:{1}", from, message + Environment.NewLine);
            this.chatManager.AddMessage(message);
        }
    }
}
