using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class ExceptionTry<T> : Try<T>
	{
		public override T Value => throw new InvalidOperationException("No value available.");
		public override bool HasException => true;
		public override bool HasValue => false;
		public override Exception? Exception { get; }

		public ExceptionTry(Exception exception)
		{
			Exception = exception;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
		{
			return new ExceptionTry<TResult>(Exception!);
		}

		public override TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue)
		{
			return whenException(Exception!);
		}

		public override void Match(Action<Exception> whenException, Action<T> whenValue)
		{
			whenException(Exception!);
		}
	}
}