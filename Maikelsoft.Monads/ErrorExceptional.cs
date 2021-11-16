using System;

namespace Maikelsoft.Monads
{
	internal sealed class ErrorExceptional<T> : IExceptional<T>
		where T : IEquatable<T>
	{
		public T Value => throw new InvalidOperationException("Cannot get value in case of error.");
		public bool HasError => true;
		public bool HasValue => false;
		public string? ErrorMessage { get; }

		public ErrorExceptional(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public IExceptional<TResult> Bind<TResult>(Func<T, IExceptional<TResult>> bind) where TResult 
			: IEquatable<TResult>
		{
			return new ErrorExceptional<TResult>(ErrorMessage!);
		}

		public TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenError(ErrorMessage!);
		}

		public void Match(Action<string> whenError, Action<T> whenValue)
		{
			whenError(ErrorMessage!);
		}

		public override int GetHashCode()
		{
			return ErrorMessage!.GetHashCode();
		}

		public bool Equals(IExceptional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ErrorMessage!.Equals(other.ErrorMessage, StringComparison.Ordinal);
		}
	}
}