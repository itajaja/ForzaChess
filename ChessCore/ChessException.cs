using System;
using System.Runtime.Serialization;

namespace ForzaChess.Core
{
    public class ChessException : Exception
    {
        public ChessException()
      {
         // Add any type-specific logic, and supply the default message.
      }

      public ChessException(string message): base(message) 
      {
         // Add any type-specific logic.
      }
      public ChessException(string message, Exception innerException): 
         base (message, innerException)
      {
         // Add any type-specific logic for inner exceptions.
      }
      protected ChessException(SerializationInfo info, 
         StreamingContext context) : base(info, context)
      {
         // Implement type-specific serialization constructor logic.
      }
    }
}