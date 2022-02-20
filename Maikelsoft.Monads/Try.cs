using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
    public static class Try
    {
        [Pure]
        public static Try<T> FromException<T>(Exception exception) where T : notnull
        {
            Error error = Error.FromException(exception);
            return FromError<T>(error);
        }

        [Pure]
        public static Try<T> FromError<T>(string errorMessage, string? details = null) where T : notnull
        {
            Error error = new Error(errorMessage, details);
            return FromError<T>(error);
        }

        [Pure]
        public static Try<T> Create<T>(Func<T> func) where T : notnull
        {
            Either<Error, T> result = GetResult(func);
            return new Try<T>(result);
        }

        [Pure]
        public static async Task<Try<T>> Create<T>(Func<Task<T>> func) where T : notnull
        {
            Either<Error, T> result = await GetResult(func);
            return new Try<T>(result);
        }

        [Pure]
        public static Try<T> FromError<T>(Error error) where T : notnull
        {
            Either<Error, T> result = Either<Error, T>.FromLeft(error);
            return new Try<T>(result);
        }

        [Pure]
        public static Try<T> FromValue<T>(T value) where T : notnull
        {
            Either<Error, T> result = Either<Error, T>.FromRight(value);
            return new Try<T>(result);
        }

        private static Either<Error, T> GetResult<T>(Func<T> func) where T : notnull
        {
            try
            {
                T value = func();
                return Either<Error, T>.FromRight(value);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Error error = Error.FromException(exception);
                return Either<Error, T>.FromLeft(error);
            }
        }

        private static async Task<Either<Error, T>> GetResult<T>(Func<Task<T>> func) where T : notnull
        {
            try
            {
                T value = await func().ConfigureAwait(false);
                return Either<Error, T>.FromRight(value);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Error error = Error.FromException(exception);
                return Either<Error, T>.FromLeft(error);
            }
        }
    }
}