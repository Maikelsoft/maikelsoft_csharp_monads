using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
    public sealed class Try<T> : IEquatable<Try<T>>
        where T : notnull
    {
        public Either<Error, T> Result { get; }
        public bool HasError => Result.HasLeft;
        public bool HasValue => Result.HasRight;

        internal Try(Either<Error, T> result)
        {
            Result = result;
        }

        [Pure]
        public Try<TResult> Map<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            Either<Error, TResult> result = Result.MapRight(selector);
            return new Try<TResult>(result);
        }

        [Pure]
        public async Task<Try<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull
        {
            Either<Error, TResult> result = await Result.MapRightAsync(selector);
            return new Try<TResult>(result);
        }

        [Pure]
        public Try<TResult> TryMap<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            return Result.Match(Try.FromError<TResult>, value => Try.Create(() => selector(value)));
        }

        [Pure]
        public Task<Try<TResult>> TryMapAsync<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull
        {
            return Result.MatchAsync(error =>
            {
                Try<TResult> result = Try.FromError<TResult>(error);
                return Task.FromResult(result);
            }, value => Try.Create(() => selector(value)));
        }

        public TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue) =>
            Result.Match(whenError, whenValue);

        public void Match(Action<Error> whenError, Action<T> whenValue) =>
            Result.Match(whenError, whenValue);

        public Task MatchAsync(Func<Error, Task> whenError, Func<T, Task> whenValue) =>
            Result.MatchAsync(whenError, whenValue);

        public Task<TResult> MatchAsync<TResult>(Func<Error, Task<TResult>> whenError, Func<T, Task<TResult>> whenValue) =>
            Result.MatchAsync(whenError, whenValue);

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