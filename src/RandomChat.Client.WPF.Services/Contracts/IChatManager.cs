namespace RandomChat.Client.Services.Contracts
{
    using System;
    using Models;

    public interface IChatManager
    {
        bool IsInChat { get; }

        event Action JoinedChat;
        event Action LeftChat;

        event Action<Message> NewMessage;

        void FindChat();

        void LeaveChat();

        void AddMessage(string content);
    }
}
