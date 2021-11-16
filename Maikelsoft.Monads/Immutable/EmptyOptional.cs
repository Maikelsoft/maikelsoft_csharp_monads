using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class EmptyOptional<T> : Optional<T>
		where T : IEquatable<T>
	{
		public override T Value => throw new InvalidOperationException("Cannot get value.");
		public override bool HasValue => false;

		public override int GetHashCode() => typeof(T).GetHashCode();

		public override bool Equals(Optional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return !other.HasValue;
		}

		public override void Match(Action whenEmpty, Action<T> whenValue)
		{
			whenEmpty();
		}
	}
}