using System;

namespace Maikelsoft.Monads
{
	internal sealed class ValueExceptional<T> : IExceptional<T>
		where T : IEquatable<T>
	{
		public T Value { get; }
		public bool HasError => false;
		public string? ErrorMessage => null;

		public ValueExceptional(T value)
		{
			Value = value;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public bool Equals(IExceptional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Value.Equals(other.Value);
		}
	}
}