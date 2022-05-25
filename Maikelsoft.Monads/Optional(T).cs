using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
    public abstract class Optional<T> : IEquatable<Optional<T>>
        where T : notnull
    {
        public static Optional<T> Empty => EmptyOptional<T>.Instance;

        public abstract T GetValueOrDefault(T defaultValue);

        public abstract bool Equals(Optional<T>? other);

        [Pure]
        public abstract Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
            where TResult : notnull;

        public abstract void WhenValue(Action<T> action);
        public abstract Task WhenValueAsync(Func<T, Task> func);
        public abstract void Match(Action whenEmpty, Action<T> whenValue);
        public abstract Task MatchAsync(Func<Task> whenEmpty, Func<T, Task> whenValue);
        public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

        public abstract Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> whenEmpty,
            Func<T, Task<TResult>> whenValue);

        [Pure]
        public Optional<TResult> Map<TResult>(Func<T, TResult> selector)
            where TResult : notnull
            => Bind(value => new ValueOptional<TResult>(selector(value)));
    }
}