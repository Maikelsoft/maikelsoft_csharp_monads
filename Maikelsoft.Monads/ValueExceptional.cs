using System;

namespace Maikelsoft.Monads
{
	internal sealed class ValueExceptional<T> : IExceptional<T>
		where T : IEquatable<T>
	{
		public T Value { get; }
		public bool HasError => false;
		public bool HasValue => true;
		public string? ErrorMessage => null;

		public ValueExceptional(T value)
		{
			Value = value;
		}

		public IExceptional<TResult> Bind<TResult>(Func<T, IExceptional<TResult>> bind) where TResult 
			: IEquatable<TResult>
		{
			return bind(Value);
		}

		public TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}

		public void Match(Action<string> whenError, Action<T> whenValue)
		{
			whenValue(Value);
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