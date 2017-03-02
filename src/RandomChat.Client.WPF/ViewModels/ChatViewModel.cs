namespace RandomChat.Client.WPF.ViewModels
{
    using Prism.Regions;

    public class ChatViewModel : BaseViewModel
    {
        public ChatViewModel(IRegionManager regionManager)
            : base(regionManager)
        {
        }
    }
}
