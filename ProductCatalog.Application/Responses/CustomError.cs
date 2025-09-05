using System;
namespace ProductCatalog.Application.Responses
{
    public sealed record CustomError(string code, string message)
    {
        private static readonly string _recordNotFound = "RecordNotFound";
        private static readonly string _validationErrorCode = "ValidationErrror";
        private static readonly string _loginFailedErrorCode = "LoginFailed";
        private static readonly string _registerFailedErrorCode = "RegistrationFailed";
        private static readonly string _orderFailed = "OrderFailed";

      



        public static CustomError None = new(string.Empty, string.Empty);

        public static CustomError OrderFailed(string message) => new CustomError(_orderFailed, message);

        public static CustomError RegisterFailed(string message) => new CustomError(_registerFailedErrorCode, message);

        public static CustomError LoginFailed(string message) => new CustomError(_loginFailedErrorCode, message);

        public static CustomError RecordNotFound(string message) => new CustomError(_recordNotFound, message);

        public static CustomError ValidationError(string message) => new CustomError(_validationErrorCode, message);

    }
}
