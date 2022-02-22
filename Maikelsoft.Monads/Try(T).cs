using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public sealed class Try<T> : IEquatable<Try<T>>, IOptional<T>
        where T : notnull
    {
        public Either<Error, T> Result { get; }
        public bool HasError => Result.HasLeft;
        public bool HasValue => Result.HasRight;
        public Error Error => Result.Left;
        public T Value => Result.Right;

        internal Try(Either<Error, T> result)
        {
            Result = result;
        }

        [Pure]
        public Try<TResult> Map<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            return Result.Match(Try.FromError<TResult>, value => Try.FromValue(selector(value)));
        }

        [Pure]
        public Try<TResult> TryMap<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            return Result.Match(Try.FromError<TResult>, value => Try.Create(() => selector(value)));
        }

        public TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue) =>
            Result.Match(whenError, whenValue);

        public void Match(Action<Error> whenError, Action<T> whenValue) =>
            Result.Match(whenError, whenValue);

        public T GetValueOrDefault(T defaultValue) => Result.GetRightOrDefault(defaultValue);

        #region Equality members

        public bool Equals(Try<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Result.Equals(other.Result);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Try<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Result.GetHashCode();
        }

        #endregion
    }
}