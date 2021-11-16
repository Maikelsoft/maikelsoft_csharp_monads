using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	// TODO: Add stack trace / details property
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
		public abstract string? ErrorMessage { get; }

		/// <summary>
		/// Binds the wrapped value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		public abstract Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
			where TResult : IEquatable<TResult>;

		public abstract TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
			where TResult : IEquatable<TResult>;

		public abstract void Match(Action<string> whenError, Action<T> whenValue);

		public abstract bool Equals(Try<T>? other);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> Select<TResult>(Func<T, TResult> selectorThatCanThrowException)
			where TResult : IEquatable<TResult>
			=> Bind(value => Try.Create(() => selectorThatCanThrowException(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> bind,
			Func<T, TOther, TResult> selectorThatCanThrowException)
			where TOther : IEquatable<TOther>
			where TResult : IEquatable<TResult>
			=> Bind(a => bind(a).Select(b => selectorThatCanThrowException(a, b)));
	}
}