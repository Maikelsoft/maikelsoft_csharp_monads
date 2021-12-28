using System;

namespace Maikelsoft.Monads
{
	internal sealed class ValueOptional<T> : Optional<T>
		where T : notnull
	{
		public override T Value { get; }
		public override bool HasValue => true;

		public ValueOptional(T value)
		{
			Value = value;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is ValueOptional<T> other && other.HasValue && Value.Equals(other.Value);
		}

        public override T GetValueOrDefault(T defaultValue)
        {
            return Value;
        }

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

		public override Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
		{
			return bind(Value);
		}

		public override TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}
	}
}