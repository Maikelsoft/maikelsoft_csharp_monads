using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public abstract class Try<T> : IEquatable<Try<T>>, IOptional<T>
		where T : notnull
	{
		/// <summary>
		/// 
		/// </summary>
		public abstract bool HasValue { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract T Value { get; }

        public abstract T GetValueOrDefault(T defaultValue);

        /// <summary>
		/// 
		/// </summary>
		public abstract bool HasError { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract Error? Error { get; }

		/// <summary>
		/// Binds the wrapped value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		public abstract Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind) where TResult : notnull;

		public void Deconstruct(out Error? error, out T value)
		{
			error = Error;
			value = Value;
		}

		public abstract TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue);

		public abstract void Match(Action<Error> whenError, Action<T> whenValue);

		public abstract bool Equals(Try<T>? other);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
			Bind(value => Try.Create(() => selector(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="trySelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> trySelector,
			Func<T, TOther, TResult> resultSelector) 
			where TOther : notnull 
			where TResult : notnull =>
			Bind(value1 => trySelector(value1).Select(value2 => resultSelector(value1, value2)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="other"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> CombineWith<TOther, TResult>(Try<TOther> other, Func<T, TOther, TResult> resultSelector)
			where TOther : notnull 
			where TResult : notnull =>
			Bind(value1 => other.Select(value2 => resultSelector(value1, value2)));
	}
}