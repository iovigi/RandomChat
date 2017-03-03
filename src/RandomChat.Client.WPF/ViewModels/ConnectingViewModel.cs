namespace RandomChat.Client.WPF.ViewModels
{
    using Prism.Regions;

    public class ConnectingViewModel : BaseViewModel
    {
        private string state;

        public ConnectingViewModel(IRegionManager regionManager)
            : base(regionManager)
        {
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
    }
}
