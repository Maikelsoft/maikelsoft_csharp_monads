using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Maikelsoft.Monads.TryImpl;

namespace Maikelsoft.Monads
{
	// This generic class should not reference the non-generic (static) class.
	public abstract class Try<T> : IEquatable<Try<T>>, IOptional<T>
		where T : notnull
	{
		public abstract bool HasValue { get; }
		public abstract T Value { get; }
		public abstract bool HasError { get; }
		public abstract Error Error { get; }

        #region Factory methods (internal)

        internal static Try<T> Create(Func<T> func)
        {
            try
            {
                T value = func();
                return new ValueTry<T>(value);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return new ErrorTry<T>(Error.FromException(exception));
            }
        }

        internal static async Task<Try<T>> Create(Func<Task<T>> func)
        {
            try
            {
                var value = await func().ConfigureAwait(false);
                return new ValueTry<T>(value);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return new ErrorTry<T>(Error.FromException(exception));
            }
        }

        internal static Try<T> FromValue(T value) => new ValueTry<T>(value);

        internal static Try<T> FromException(Exception exception)
        {
            return new ErrorTry<T>(Error.FromException(exception));
        }

        internal static Try<T> FromError(Error error) => new ErrorTry<T>(error);
		
        internal static Try<T> FromError(string errorMessage, string? details = null)
        {
            return new ErrorTry<T>(new Error(errorMessage, details));
        }

        #endregion

        #region Abstract methods

        public abstract T GetValueOrDefault(T defaultValue);

        [Pure]
        public abstract Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind) where TResult : notnull;
		
        public abstract TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue);

        public abstract void Match(Action<Error> whenError, Action<T> whenValue);

        public abstract bool Equals(Try<T>? other);

        #endregion

		[Pure]
		public Try<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
			Bind(value => Try<TResult>.Create(() => selector(value)));

		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> trySelector,
			Func<T, TOther, TResult> resultSelector) 
			where TOther : notnull 
			where TResult : notnull =>
			Bind(value1 => trySelector(value1).Select(value2 => resultSelector(value1, value2)));

		[Pure]
		public Try<TResult> CombineWith<TOther, TResult>(Try<TOther> other, Func<T, TOther, TResult> resultSelector)
			where TOther : notnull 
			where TResult : notnull =>
			Bind(value1 => other.Select(value2 => resultSelector(value1, value2)));
	}
}