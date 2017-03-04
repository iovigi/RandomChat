namespace RandomChat.Client.WPF
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Regions;
    using Common.Client;
    using ViewModels;
    using Services.Contracts;

    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IRegionManager regionManager, IServerManager serverManager)
            : base(regionManager, serverManager)
        {
            this.InitializeCommand = new DelegateCommand(this.Initialize);
            this.ClosingCommand = new DelegateCommand(this.Closing);
        }

        public ICommand InitializeCommand { get; set; }
        public ICommand ClosingCommand { get; set; }

        private void Initialize()
        {
            this.serverManager.Connect();
            this.RequestNavigation(ViewConstants.CONNECTING_VIEW_NAME);
        }

        private void Closing()
        {
            this.serverManager.Disconnect();
        }
    }
}
