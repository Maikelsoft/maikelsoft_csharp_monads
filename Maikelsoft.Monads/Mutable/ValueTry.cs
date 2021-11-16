using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class ValueTry<T> : Try<T>
	{
		public override T Value { get; }
		public override bool HasException => false;
		public override bool HasValue => true;
		public override Exception? Exception => null;

		public ValueTry(T value)
		{
			Value = value;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
		{
			return bind(Value);
		}

		public override TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue)
		{
			return whenValue(Value);
		}

		public override void Match(Action<Exception> whenException, Action<T> whenValue)
		{
			whenValue(Value);
		}
	}
}