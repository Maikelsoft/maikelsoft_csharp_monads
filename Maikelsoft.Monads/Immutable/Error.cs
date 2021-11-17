using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
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

        public bool Equals(Error? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Message.Equals(other.Message) &&
                Nullable.Equals(Details, other.Details);
        }
    }
}