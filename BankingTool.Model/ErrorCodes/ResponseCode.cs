namespace BankingTool.Model.ErrorCodes
{
    public enum ResponseCode
    {
        // Standard HTTP Status
        ServerError_InternalServerError = 500,
        ServerError_BadRequest = 400,
        ServerError_ValidationFailed = 401,
        ServerError_NotFound = 404,
        ServerSuccess_Success = 200,
        ServerSuccess_Created = 201,
        ServerSuccess_MultiStatus = 207,

        // Custom Application Error Codes
        ResponseCode_LoginFailed = 1000
    }
}
