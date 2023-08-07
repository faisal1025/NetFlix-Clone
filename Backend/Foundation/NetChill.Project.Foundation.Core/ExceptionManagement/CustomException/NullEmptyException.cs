using System;
using System.Runtime.Serialization;

namespace Company.Project.Core.ExceptionManagement.CustomException
{
    [Serializable]
    public class NullEmptyException : Exception
    {
        public NullEmptyException()
        {
        }

        public NullEmptyException(string message) : base(message)
        {
        }

        public NullEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}