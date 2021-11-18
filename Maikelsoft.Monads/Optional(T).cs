using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public abstract class Optional<T> : IEquatable<Optional<T>>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public abstract bool Equals(Optional<T>? other);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="whenEmpty"></param>
		/// <param name="whenValue"></param>
		public abstract void Match(Action whenEmpty, Action<T> whenValue);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		public abstract Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
			where TResult : notnull;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="whenEmpty"></param>
		/// <param name="whenValue"></param>
		/// <returns></returns>
		public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

		public static Optional<T> Empty => new EmptyOptional<T>();

		[Pure]
		public static Optional<T> From(T value) => new ValueOptional<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public Optional<TResult> Select<TResult>(Func<T, TResult> selector)
			where TResult : notnull
			=> Bind(value => Optional<TResult>.From(selector(value)));

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
		public Optional<TResult> SelectMany<TOther, TResult>(Func<T, Optional<TOther>> bind,
			Func<T, TOther, TResult> selector)
			where TOther : notnull
			where TResult : notnull => 
			Bind(value1 => bind(value1).Select(value2 => selector(value1, value2)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="other"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public Optional<TResult> CombineWith<TOther, TResult>(Optional<TOther> other, Func<T, TOther, TResult> resultSelector)
			where TOther : notnull
			where TResult : notnull =>
			Bind(value1 => other.Select(value2 => resultSelector(value1, value2)));
	}
}