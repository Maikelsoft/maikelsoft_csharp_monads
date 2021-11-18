using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public sealed class Error : IEquatable<Error>
    {
        private readonly int _hashCode;
		public string Message { get; }
		public string? Details { get; }

        public Error(string message, string? details)
        {
            Message = message;
            Details = details;
            _hashCode = HashCode.Combine(message, details);
        }

        [Pure]
        public static Error FromException(Exception exception)
        {
            return new Error(exception.Message, exception.StackTrace);
        }

        public override int GetHashCode() => _hashCode;

        public override bool Equals(object? obj)
        {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        return obj is Error other && 
	               Message.Equals(other.Message) && 
	               Nullable.Equals(Details, other.Details);
        }

        public bool Equals(Error? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Message.Equals(other.Message) &&
                   Nullable.Equals(Details, other.Details);
        }
    }
}