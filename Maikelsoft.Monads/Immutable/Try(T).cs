using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	public abstract class Try<T> : IEquatable<Try<T>>
		where T : IEquatable<T>
	{
		/// <summary>
		/// 
		/// </summary>
		public abstract bool HasValue { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract T Value { get; }

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
		public abstract Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
			where TResult : IEquatable<TResult>;

		public abstract TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue)
			where TResult : IEquatable<TResult>;

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
		public Try<TResult> Select<TResult>(Func<T, TResult> selector)
			where TResult : IEquatable<TResult>
			=> Bind(value => Try.Create(() => selector(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> bind,
			Func<T, TOther, TResult> selector)
			where TOther : IEquatable<TOther>
			where TResult : IEquatable<TResult>
			=> Bind(a => bind(a).Select(b => selector(a, b)));
	}
}