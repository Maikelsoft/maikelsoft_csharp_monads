using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
    public abstract class Optional<T> : IEquatable<Optional<T>>
        where T : notnull
    {
        [Pure]
        public static Optional<T> Empty => EmptyOptional<T>.Instance;

        [Obsolete("Use Match() or GetValueOrDefault()")]
        public abstract bool HasValue { get; }

        [Obsolete("Use Match() or GetValueOrDefault()")]
        public abstract T Value { get; }

        public abstract T GetValueOrDefault(T defaultValue);

        public abstract bool Equals(Optional<T>? other);

        [Pure]
        public abstract Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
            where TResult : notnull;

        public abstract void WhenValue(Action<T> action);

        public abstract void Match(Action whenEmpty, Action<T> whenValue);

        public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

        [Pure]
        public Optional<TResult> Map<TResult>(Func<T, TResult> selector)
            where TResult : notnull
            => Bind(value => new ValueOptional<TResult>(selector(value)));
    }
}