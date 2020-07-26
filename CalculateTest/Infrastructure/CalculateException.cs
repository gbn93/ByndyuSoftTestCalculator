using System;
using System.Collections.Generic;
using System.Text;

namespace Calculate.Infrastructure
{
    public class CalculateException : Exception
    {
        public CalculateException() { }

        public CalculateException(string message) : base(message) { }

        public CalculateException(string message, Exception inner) : base(message, inner) { }

        protected CalculateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
