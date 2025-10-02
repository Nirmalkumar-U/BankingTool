namespace BankingTool.Model.ErrorCodes
{
    public static class ResponseMessageTitle
    {
        // Standard HTTP Status Codes Messages Titles
        public const string ServerError_InternalServerError = "Internal Server Error.";
        public const string ServerError_BadRequest = "Bad Request.";
        public const string ServerError_ValidationFailed = "Validation Failed.";
        public const string ServerError_NotFound = "Not Found.";
        public const string ServerSuccess_Success = "Success.";
        public const string ServerSuccess_Created = "Created.";
        public const string ServerSuccess_MultiStatus = "Multi Status.";

        // Custom Application Error Codes Messages
        public const string ResponseCode_LoginFailed = "Login Failed.";

        public static string GetMessageTitle(this ResponseCode code)
        {
            return code switch
            {
                ResponseCode.ServerError_InternalServerError => ServerError_InternalServerError,
                ResponseCode.ServerError_BadRequest => ServerError_BadRequest,
                ResponseCode.ServerError_ValidationFailed => ServerError_ValidationFailed,
                ResponseCode.ServerError_NotFound => ServerError_NotFound,
                ResponseCode.ServerSuccess_Success => ServerSuccess_Success,
                ResponseCode.ServerSuccess_Created => ServerSuccess_Created,
                ResponseCode.ServerSuccess_MultiStatus => ServerSuccess_MultiStatus,
                ResponseCode.ResponseCode_LoginFailed => ResponseCode_LoginFailed,
                _ => "Unknown Status"
            };
        }
    }
}
