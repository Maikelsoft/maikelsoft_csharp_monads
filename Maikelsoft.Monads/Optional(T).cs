using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
    public abstract class Optional<T> : IEquatable<Optional<T>>, IOptional<T>
        where T : notnull
    {
        public static Optional<T> Empty => EmptyOptional<T>.Instance;

        public abstract bool HasValue { get; }

        public abstract T Value { get; }

        public abstract T GetValueOrDefault(T defaultValue);

        public abstract bool Equals(Optional<T>? other);

        [Pure]
        public abstract Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
            where TResult : notnull;

        public abstract void Match(Action whenEmpty, Action<T> whenValue);

        public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

        internal static Optional<T> From(T value) => new ValueOptional<T>(value);

        [Pure]
        public Optional<TResult> Map<TResult>(Func<T, TResult> selector)
            where TResult : notnull
            => Bind(value => Optional<TResult>.From(selector(value)));
    }
}