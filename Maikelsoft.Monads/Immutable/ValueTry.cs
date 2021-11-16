using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class ValueTry<T> : Try<T>
		where T : IEquatable<T>
	{
		public override T Value { get; }
		public override bool HasError => false;
		public override bool HasValue => true;
		public override string? ErrorMessage => null;

		public ValueTry(T value)
		{
			Value = value;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind) 
		{
			return bind(Value);
		}

		public override TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}

		public override void Match(Action<string> whenError, Action<T> whenValue)
		{
			whenValue(Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(Try<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasValue && Value.Equals(other.Value);
		}
	}
}