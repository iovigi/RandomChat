namespace RandomChat.Client.WPF
{
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Regions;
    using Prism.Mvvm;
    using Common.Client;

    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.InitializeCommand = new DelegateCommand(Initialize);
        }

        public ICommand InitializeCommand { get; set; }

        private void Initialize()
        {
            this.regionManager.RequestNavigate(RegionConstants.MAIN_REGION_NAME, ViewConstats.CHAT_VIEW_NAME);
        }
    }
}
