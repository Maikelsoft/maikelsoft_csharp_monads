using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class ValueTry<T> : ITry<T>
	{
		public T Value { get; }
		public bool HasException => false;
		public bool HasValue => true;
		public Exception? Exception => null;

		public ValueTry(T value)
		{
			Value = value;
		}

		public ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind)
		{
			return bind(Value);
		}

		public TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}

		public void Match(Action<Exception> whenException, Action<T> whenValue)
		{
			whenValue(Value);
		}
	}
}