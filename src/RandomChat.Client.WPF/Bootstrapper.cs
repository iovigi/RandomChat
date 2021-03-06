﻿namespace RandomChat.Client.WPF
{
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using Microsoft.Practices.Unity;
    using Prism.Unity;
    using Common;
    using Common.Client;
    using Views;
    using Services;
    using Services.Contracts;

    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var serverName = ConfigurationManager.AppSettings[ConfigurationConstants.SERVER_CONFIG_NAME];

            this.Container.RegisterType<IRestClient, RestClient>(new InjectionConstructor(serverName));
            this.Container.RegisterType<IServerManager, ServerManager>(new ContainerControlledLifetimeManager());

            var assembly = Assembly.Load(Assemblies.RANDOM_CHAT_CLIENT_WPF);
            var exportedTypes = assembly.GetExportedTypes();

            foreach (var t in exportedTypes.Where(x => !x.IsAbstract && !x.IsInterface && x != typeof(RestClient) && x != typeof(ServerManager)))
            {
                this.Container.RegisterType(t);

                var interfaces = t.GetInterfaces();

                foreach (var intf in interfaces)
                {
                    this.Container.RegisterType(intf, t);
                }
            }

            this.Container.RegisterType<object, ChatView>(ViewConstants.CHAT_VIEW_NAME);
            this.Container.RegisterType<object, ConnectingView>(ViewConstants.CONNECTING_VIEW_NAME);

            return this.Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
