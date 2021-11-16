using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class ExceptionTry<T> : ITry<T>
	{
		public T Value => throw new InvalidOperationException("Cannot get value in case of exception.");
		public bool HasException => true;
		public bool HasValue => false;
		public Exception? Exception { get; }

		public ExceptionTry(Exception exception)
		{
			Exception = exception;
		}

		public ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind)
		{
			return new ExceptionTry<TResult>(Exception!);
		}

		public TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue)
		{
			return whenException(Exception!);
		}

		public void Match(Action<Exception> whenException, Action<T> whenValue)
		{
			whenException(Exception!);
		}
	}
}