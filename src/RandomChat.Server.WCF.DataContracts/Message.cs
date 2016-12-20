namespace RandomChat.Server.WCF.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Message
    {
        [DataMember]
        public DateTime SendOn { get; set; }

        [DataMember]
        public string Content { get; set; }

        public int IP { get; set; }
    }
}
