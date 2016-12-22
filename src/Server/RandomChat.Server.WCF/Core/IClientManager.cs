namespace RandomChat.Server.WCF.Core
{
    using DataContracts; 

    public interface IClientManager
    {
        bool JoinClient(Client client);

        bool LeaveClient(Client client);

        Client GetFreeClient();

        void SetNotFreeClient(Client client);

        void SetFreeClient(Client client);

        void Ping(Client client);

        bool IsClientFree(Client client);
    }
}
