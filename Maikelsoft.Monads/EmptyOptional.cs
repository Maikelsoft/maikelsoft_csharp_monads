using System;

namespace Maikelsoft.Monads
{
	internal sealed class EmptyOptional<T> : Optional<T>
		where T : notnull
	{
		public override T Value => throw new InvalidOperationException("Cannot get value.");
		public override bool HasValue => false;

		public override int GetHashCode() => typeof(T).GetHashCode();

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is EmptyOptional<T>;
		}

		public override bool Equals(Optional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other is EmptyOptional<T>;
		}

		public override void Match(Action whenEmpty, Action<T> whenValue)
		{
			whenEmpty();
		}

		public override Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
		{
			return Optional<TResult>.Empty;
		}

		public override TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue)
		{
			return whenEmpty();
		}
	}
}