namespace RandomChat.Server.WCF.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Client
    {
        private static int nextUId = 0;

        private volatile bool isFree;

        public Client(string id)
        {
            this.ID = id;
            this.UId = nextUId++;
            this.LastPingTime = DateTime.Now;
        }

        [DataMember]
        public int UId { get; set; }

        public string ID { get; set; }

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

            return this.ID == otherClient.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public static string GetNextId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
