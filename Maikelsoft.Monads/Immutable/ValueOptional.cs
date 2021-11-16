using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class ValueOptional<T> : Optional<T>
		where T : IEquatable<T>
	{
		public override T Value { get; }
		public override bool HasValue => true;

		public ValueOptional(T value)
		{
			Value = value;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override bool Equals(Optional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasValue && Value.Equals(other.Value);
		}

		public override void Match(Action whenEmpty, Action<T> whenValue)
		{
			whenValue(Value);
		}

		public override TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}
	}
}