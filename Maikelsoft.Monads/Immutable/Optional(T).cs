using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	// TODO: Add Match method
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

		public abstract bool Equals(Optional<T>? other);

		public abstract void Match(Action whenEmpty, Action<T> whenValue);

		public static Optional<T> Empty => new EmptyOptional<T>();

		[Pure]
		public static Optional<T> From(T value) => new ValueOptional<T>(value);
	}
}