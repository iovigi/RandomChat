namespace RandomChat.Client.Services.Models
{
    using System;

    public class Message
    {
        public Message()
        {
        }

        public Message(DateTime sendOn,string content)
        {
            this.SendOn = sendOn;
            this.Content = content;
        }

        public DateTime SendOn { get; set; }
        public string Content { get; set; }
    }
}
