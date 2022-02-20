using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
    public abstract class Optional<T> : IEquatable<Optional<T>>, IOptional<T>
        where T : notnull
    {
        public abstract bool HasValue { get; }

        public abstract T Value { get; }

        public abstract T GetValueOrDefault(T defaultValue);

        public abstract bool Equals(Optional<T>? other);

        [Pure]
        public abstract Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
            where TResult : notnull;

        public abstract void Match(Action whenEmpty, Action<T> whenValue);

        public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

        #region Factory methods (internal)
        
        internal static Optional<T> Empty => EmptyOptional<T>.Instance;
        internal static Optional<T> From(T value) => new ValueOptional<T>(value);

        #endregion

        [Pure]
        public Optional<TResult> Select<TResult>(Func<T, TResult> selector)
            where TResult : notnull
            => Bind(value => Optional<TResult>.From(selector(value)));

        [Pure]
        public Optional<TResult> SelectMany<TOther, TResult>(Func<T, Optional<TOther>> bind,
            Func<T, TOther, TResult> selector)
            where TOther : notnull
            where TResult : notnull =>
            Bind(value1 => bind(value1).Select(value2 => selector(value1, value2)));

        [Pure]
        public Optional<TResult> CombineWith<TOther, TResult>(Optional<TOther> other,
            Func<T, TOther, TResult> resultSelector)
            where TOther : notnull
            where TResult : notnull =>
            Bind(value1 => other.Select(value2 => resultSelector(value1, value2)));
    }
}