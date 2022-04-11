using System;

namespace Maikelsoft.Monads.OptionalImpl
{
	internal sealed class ValueOptional<T> : Optional<T>
		where T : notnull
	{
        private readonly T _value;

        public override T Value => _value;

        public override bool HasValue => true;

		public ValueOptional(T value)
		{
			_value = value;
		}

		public override int GetHashCode() => _value.GetHashCode();

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is ValueOptional<T> other && _value.Equals(other._value);
		}

        public override bool Equals(Optional<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is ValueOptional<T> optional && _value.Equals(optional._value);
        }

        public override T GetValueOrDefault(T defaultValue)
        {
            return _value;
        }

        public override void WhenValue(Action<T> action)
        {
            action(_value);
        }

        public override void Match(Action whenEmpty, Action<T> whenValue)
		{
			whenValue(_value);
		}

		public override Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
		{
			return bind(_value);
		}

		public override TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue)
		{
			return whenValue(_value);
		}
	}
}