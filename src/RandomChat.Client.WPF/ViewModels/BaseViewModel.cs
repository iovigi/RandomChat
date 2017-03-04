namespace RandomChat.Client.WPF.ViewModels
{
    using System;
    using Prism.Regions;
    using Prism.Mvvm;
    using Common.Client;
    using Services.Contracts;

    public abstract class BaseViewModel : BindableBase, INavigationAware, IDisposable
    {
        protected bool disposed;

        protected string errorMessages;

        protected readonly IRegionManager regionManager;
        protected readonly IServerManager serverManager;

        public BaseViewModel(IRegionManager regionManager, IServerManager serverManager)
        {
            this.regionManager = regionManager;
            this.serverManager = serverManager;

            this.serverManager.LostConnection += this.OnLostConnection;
        }


        public string ErrorMessages
        {
            get
            {
                return this.errorMessages;
            }
            set
            {
                this.SetProperty(ref this.errorMessages, value);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            this.disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void RequestNavigation(string viewName, string regionName = RegionConstants.MAIN_REGION_NAME)
        {
            this.regionManager.RequestNavigate(regionName, viewName);
        }

        public void GoToConnectingScreen()
        {
            this.RequestNavigation(ViewConstants.CONNECTING_VIEW_NAME);
        }

        protected virtual void OnLostConnection()
        {
            this.GoToConnectingScreen();
        }
    }
}
