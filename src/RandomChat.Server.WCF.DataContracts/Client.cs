namespace RandomChat.Server.WCF.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Client
    {
        private static int nextUId = 0;

        private volatile bool isFree;

        public Client(string ip)
        {
            this.IP = ip;
            this.UId = nextUId++;
            this.LastPingTime = DateTime.Now;
        }

        [DataMember]
        public int UId { get; set; }

        public string IP { get; set; }

        public DateTime LastPingTime { get; set; }

        public bool IsFree
        {
            get
            {
                return this.isFree;
            }
            set
            {
                this.isFree = value;
            }
        }

        public override bool Equals(object obj)
        {
            var otherClient = obj as Client;

            if(otherClient == null)
            {
                return false;
            }

            return this.IP == otherClient.IP;
        }

        public override int GetHashCode()
        {
            return this.IP.GetHashCode();
        }
    }
}
