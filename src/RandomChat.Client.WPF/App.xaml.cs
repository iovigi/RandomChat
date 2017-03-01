namespace RandomChat.Client.WPF
{
    using System.Windows;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}
