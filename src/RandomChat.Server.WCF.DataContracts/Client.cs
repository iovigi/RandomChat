namespace RandomChat.Server.WCF.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Client
    {
        private static int nextUId = 0;
        public Client()
        {
            this.UId = nextUId++;
        }

        [DataMember]
        public int UId { get; set; }

        public string IP { get; set; }

        public override bool Equals(object obj)
        {
            var otherClient = obj as Client;

            if(otherClient == null)
            {
                return false;
            }

            return this.UId == otherClient.UId;
        }

        public override int GetHashCode()
        {
            return this.UId.GetHashCode();
        }
    }
}
