using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace InnoCVApi.Core.Common
{
    public static class ExceptionExtensions
    {
        public static Exception GetInnerException(this Exception exception)
        {
            var innerException = exception;

            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            return innerException;
        }

        public static bool IsSocketException(this Exception exception)
        {
            if (exception is SocketException)
            {
                return true;
            }

            return exception.InnerException != null && IsSocketException(exception.InnerException);
        }

        public static bool IsWin32Exception(this Exception exception)
        {
            if (exception is Win32Exception)
            {
                return true;
            }

            return exception.InnerException != null && IsWin32Exception(exception.InnerException);
        }

        public static bool IsSqlException(this Exception exception)
        {
            if (exception is SqlException)
            {
                return true;
            }

            return exception.InnerException != null && IsSqlException(exception.InnerException);
        }

        public static bool IsTimeout(this Exception exception)
        {
            if (exception.ToString().ToLowerInvariant().Contains("timeout"))
            {
                return true;
            }

            return exception.InnerException != null && IsTimeout(exception.InnerException);
        }
    }
}