namespace RandomChat.Server.WCF
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using Common.Server;
    using DataContracts;

    [ServiceContract]
    public interface IRandomChatService
    {
        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.JOIN_TO_SERVER_ADDRESS)]
        string JoinToServer();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.LEAVE_SERVER_ADDRESS)]
        void LeaveServer();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.PING_ADDRESS)]
        void Ping();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.FIND_FREE_CLIENT_TO_CHAT_ADDRESS)]
        void FindFreeClientToChat();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.LEAVE_CHAT_ADDRESS)]
        void LeaveChat();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.IS_IN_CHAT_ADDRESS)]
        bool IsInChat();

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.ADD_MESSAGE_ADDRESS)]
        bool AddMessage(string message, DateTime sendOn);

        [OperationContract]
        [WebInvoke(Method = ServiceConstants.METHOD_POST, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = ServiceConstants.FIND_FREE_CLIENT_TO_CHAT_ADDRESS)]
        IEnumerable<Message> GetMessageFromOtherClientAfter(DateTime date);
    }
}
