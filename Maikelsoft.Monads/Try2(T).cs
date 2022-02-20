using System;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
    public sealed class Try2<T>
        where T : notnull
    {
        public Either<Error, T> Result { get; }
        public bool HasError => Result.HasLeft;
        public bool HasValue => Result.HasRight;
        public Error Error => Result.Left;
        public T Value => Result.Right;
        
        private Try2(Either<Error, T> result)
        {
            Result = result;
        }

        #region Factory methods

        internal static Try2<T> Create(Func<T> func)
        {
            Either<Error, T> result = GetResult(func);
            return new Try2<T>(result);
        }

        internal static async Task<Try2<T>> Create(Func<Task<T>> func)
        {
            Either<Error, T> result = await GetResult(func);
            return new Try2<T>(result);
        }

        internal static Try2<T> FromError(Error error)
        {
            Either<Error, T> result = Either<Error, T>.FromLeft(error);
            return new Try2<T>(result);
        }

        internal static Try2<T> FromValue(T value)
        {
            Either<Error, T> result = Either<Error, T>.FromRight(value);
            return new Try2<T>(result);
        }

        internal static Try2<T> FromException(Exception exception) => 
            FromError(Error.FromException(exception));
        
        internal static Try2<T> FromError(string errorMessage, string? details = null)
        {
            Error error = new Error(errorMessage, details);
            return FromError(error);
        }

        #endregion
        
        private static Either<Error, T> GetResult(Func<T> func)
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

        private static async Task<Either<Error, T>> GetResult(Func<Task<T>> func)
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