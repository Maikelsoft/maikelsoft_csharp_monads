using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class ValueTry<T> : ITry<T>
		where T : IEquatable<T>
	{
		public T Value { get; }
		public bool HasError => false;
		public bool HasValue => true;
		public string? ErrorMessage => null;

		public ValueTry(T value)
		{
			Value = value;
		}

		public ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind) where TResult 
			: IEquatable<TResult>
		{
			return bind(Value);
		}

		public TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
			where TResult : IEquatable<TResult>
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

		public bool Equals(ITry<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasValue && Value.Equals(other.Value);
		}
	}
}