﻿using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public sealed class Error : IEquatable<Error>
    {
        private readonly int _hashCode;
        public string Message { get; }
        public string? Details { get; }
        public Error? InnerError { get; }

        public Error(string message, string? details = null, Error? innerError = null)
        {
            Message = message;
            Details = details;
            InnerError = innerError;
            _hashCode = HashCode.Combine(message, details);
        }

        [Pure]
        public static Error FromException(Exception exception)
        {
            Error? innerError = exception.InnerException != null 
                ? FromException(exception.InnerException) 
                : null;

            return new Error(exception.Message, exception.StackTrace, innerError);
        }

        public override int GetHashCode() => _hashCode;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Error other && Message.Equals(other.Message) && Details == other.Details;
        }

        public bool Equals(Error? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Message.Equals(other.Message) && Details == other.Details;
        }
    }
}