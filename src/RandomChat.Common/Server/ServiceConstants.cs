namespace RandomChat.Common.Server
{
    public class ServiceConstants
    {
        public const string METHOD_POST = "POST";
        public const string METHOD_GET = "GET";

        //Addresses of service
        public const string JOIN_TO_SERVER_ADDRESS = "JoinToServer";
        public const string LEAVE_SERVER_ADDRESS = "LeaveServer";
        public const string PING_ADDRESS = "Ping";
        public const string FIND_FREE_CLIENT_TO_CHAT_ADDRESS = "FindFreeClientToChat";
        public const string LEAVE_CHAT_ADDRESS = "LeaveChat";
        public const string IS_IN_CHAT_ADDRESS = "IsInChat";
        public const string ADD_MESSAGE_ADDRESS = "AddMessage";
        public const string GET_MESSAGE_FROM_OTHER_CLIENT_AFTER_ADDRESS = "GetMessageFromOtherClientAfter";
    }
}
