using System;

namespace Maikelsoft.Monads
{
	internal sealed class ValueTry<T> : Try<T>
		where T : notnull
	{
		public override T Value { get; }
        public override T GetValueOrDefault(T defaultValue) => Value;

        public override bool HasError => false;
		public override bool HasValue => true;
		public override Error? Error => null;

		public ValueTry(T value)
		{
			Value = value;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind) 
		{
			return bind(Value);
		}

		public override TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}

		public override void Match(Action<Error> whenError, Action<T> whenValue)
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