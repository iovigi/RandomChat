namespace RandomChat.Server.Console
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using WCF;

    public class StartUp
    {
        public static void Main()
        {
            Uri baseAddress = new Uri("http://localhost:8080/");

            using (ServiceHost host = new ServiceHost(typeof(RandomChatService), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);

                while (true)
                {
                    Console.WriteLine("Type 'exit' to stop the service.");
                    var cmd = Console.ReadLine();

                    if (cmd == "exit")
                    {
                        break;
                    }
                }

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
