﻿namespace RandomChat.Server.WCF.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataContracts;
    using System.Threading.Tasks;
    using System.Threading;

    public class ClientManager : IClientManager
    {
        protected object SyncRoot = new object();

        private const int CLEAN_TIME = 10 * 1000;//10 seconds
        private const int CLIENT_CLEAN_TIME = 10 * 1000;//10 seconds

        private readonly HashSet<Client> clients;
        private Task CleanTask;

        public ClientManager()
        {
            this.clients = new HashSet<Client>();
            this.CleanTask = Task.Factory.StartNew(this.Clean);
        }

        public Client GetClient(string ip)
        {
            lock (SyncRoot)
            {
                return this.clients.FirstOrDefault(x => x.IP == ip);
            }
        }

        public Client GetFreeClient()
        {
            lock (SyncRoot)
            {
                return this.clients.FirstOrDefault(x => x.IsFree);
            }
        }

        public bool JoinClient(Client client)
        {
            lock (SyncRoot)
            {
                return this.clients.Add(client);
            }
        }

        public bool LeaveClient(Client client)
        {
            lock (SyncRoot)
            {
                return this.clients.Remove(client);
            }
        }

        public void Ping(Client client)
        {
            lock (SyncRoot)
            {
                var c = this.clients.FirstOrDefault(x => x.Equals(client));

                if(c == null)
                {
                    return;
                }

                c.LastPingTime = DateTime.Now;
            }
        }

        public void SetFreeClient(Client client)
        {
            lock (SyncRoot)
            {
                var c = this.clients.FirstOrDefault(x => x.Equals(client));

                if (c == null)
                {
                    return;
                }

                c.IsFree = true;
            }
        }

        public void SetNotFreeClient(Client client)
        {
            lock (SyncRoot)
            {
                var c = this.clients.FirstOrDefault(x => x.Equals(client));

                if (c == null)
                {
                    return;
                }

                c.IsFree = false;
            }
        }

        public bool IsClientFree(Client client)
        {
            lock (SyncRoot)
            {
                var c = this.clients.FirstOrDefault(x => x.Equals(client));

                if (c == null)
                {
                    return false;
                }

                return client.IsFree;
            }
        }

        private void Clean()
        {
            while(true)
            {
                var cleanTime = DateTime.Now.AddSeconds(-CLIENT_CLEAN_TIME);

                var forRemove = new HashSet<Client>();

                lock(SyncRoot)
                {
                    foreach(var client in this.clients)
                    {
                        if(client.LastPingTime < cleanTime)
                        {
                            forRemove.Add(client);
                        }
                    }

                    foreach(var client in forRemove)
                    {
                        this.clients.Remove(client);
                    }
                }

                Thread.Sleep(CLEAN_TIME);
            }
        }
    }
}
