using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
    public sealed class Try<T> : IEquatable<Try<T>>
        where T : notnull
    {
        public Either<Error, T> Either { get; }

        internal Try(Either<Error, T> either)
        {
            Either = either;
        }

        [Pure]
        public Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
            where TResult : notnull
        {
            Either<Error, TResult> result = Either.BindRight(value => bind(value).Either);
            return new Try<TResult>(result);
        }

        [Pure]
        public Try<TResult> Map<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            Either<Error, TResult> result = Either.MapRight(selector);
            return new Try<TResult>(result);
        }

        [Pure]
        public async Task<Try<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull
        {
            Either<Error, TResult> result = await Either.MapRightAsync(selector);
            return new Try<TResult>(result);
        }

        [Pure]
        public Try<TResult> TryMap<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            return Either.Match(Try.FromError<TResult>, value => Try.Create(() => selector(value)));
        }

        [Pure]
        public Task<Try<TResult>> TryMapAsync<TResult>(Func<T, Task<TResult>> selector) where TResult : notnull
        {
            return Either.MatchAsync(error =>
            {
                Try<TResult> result = Try.FromError<TResult>(error);
                return Task.FromResult(result);
            }, value => Try.Create(() => selector(value)));
        }

        public void WhenError(Action<Error> action) => Either.WhenLeft(action);
        public Task WhenErrorAsync(Func<Error, Task> func) => Either.WhenLeftAsync(func);
        public void WhenValue(Action<T> action) => Either.WhenRight(action);
        public Task WhenValueAsync(Func<T, Task> func) => Either.WhenRightAsync(func);

        public void Match(Action<Error> whenError, Action<T> whenValue) => Either.Match(whenError, whenValue);

        public Task MatchAsync(Func<Error, Task> whenError, Func<T, Task> whenValue) =>
            Either.MatchAsync(whenError, whenValue);

        public TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue) =>
            Either.Match(whenError, whenValue);

        public Task<TResult> MatchAsync<TResult>(Func<Error, Task<TResult>> whenError,
            Func<T, Task<TResult>> whenValue) =>
            Either.MatchAsync(whenError, whenValue);

        public T GetValueOrDefault(T defaultValue) => Either.GetRightOrDefault(defaultValue);

        #region Equality members

        public bool Equals(Try<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Either.Equals(other.Either);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Try<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Either.GetHashCode();
        }

        #endregion
    }
}