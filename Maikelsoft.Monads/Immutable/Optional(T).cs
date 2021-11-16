using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	public abstract class Optional<T> : IEquatable<Optional<T>>
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
		/// <param name="whenEmpty"></param>
		/// <param name="whenValue"></param>
		/// <returns></returns>
		public abstract TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue);

		public static Optional<T> Empty => new EmptyOptional<T>();

		[Pure]
		public static Optional<T> From(T value) => new ValueOptional<T>(value);
	}
}