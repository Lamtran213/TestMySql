namespace TestMySql.Exceptions
{
    public class GlobalException : Exception
    {
        public ErrorType Type { get; }

        public GlobalException(ErrorType type, string message) : base(message)
        {
            Type = type;
        }

        public static void ThrowIfNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new GlobalException(ErrorType.BadRequest, message);
            }
        }

        public static void ThrowIfNotFound(object obj, string message)
        {
            if (obj == null)
            {
                throw new GlobalException(ErrorType.NotFound, message);
            }
        }

        public static void ThrowIfInternalError(bool condition, string message)
        {
            if (condition)
            {
                throw new GlobalException(ErrorType.InternalServerError, message);
            }
        }

        public enum ErrorType
        {
            BadRequest,
            NotFound,
            InternalServerError
        }
    }
}
