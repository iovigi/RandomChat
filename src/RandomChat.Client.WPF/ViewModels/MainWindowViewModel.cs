namespace RandomChat.Client.WPF
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Regions;
    using Common.Client;
    using ViewModels;

    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IRegionManager regionManager)
            :base(regionManager)
        {
            this.InitializeCommand = new DelegateCommand(this.Initialize);
        }

        public ICommand InitializeCommand { get; set; }

        private void Initialize()
        {
            this.RequestNavigation(ViewConstants.CONNECTING_VIEW_NAME);
        }
    }
}
