namespace BankingTool.Model.ErrorCodes
{
    public static class ResponseMessageDetails
    {
        // Standard HTTP Status Codes Messages Details
        public const string ServerError_InternalServerError = "An unexpected server-side error occurred during request processing.";
        public const string ServerError_BadRequest = "The request was malformed or invalid, preventing processing.";
        public const string ServerError_ValidationFailed = "Input data failed validation checks on the server.";
        public const string ServerError_NotFound = "The requested resource could not be found on the server.";
        public const string ServerSuccess_Success = "The request was processed successfully with no errors.";
        public const string ServerSuccess_Created = "A new resource was successfully created on the server.";
        public const string ServerSuccess_MultiStatus = "Response with multiple status outcomes for batch operations.";

        // Custom Application Error Codes Messages
        public const string ResponseCode_LoginFailed = "Login attempt failed due to invalid credentials or other issues.";

        public static string GetMessageDetails(this ResponseCode code)
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
                _ => "No additional details available."
            };
        }
    }
}
