using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class ErrorTry<T> : ITry<T>
		where T : IEquatable<T>
	{
		public T Value => throw new InvalidOperationException("Cannot get value in case of error.");
		public bool HasError => true;
		public bool HasValue => false;
		public string? ErrorMessage { get; }

		public ErrorTry(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind) where TResult 
			: IEquatable<TResult>
		{
			return new ErrorTry<TResult>(ErrorMessage!);
		}

		public TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
			where TResult : IEquatable<TResult>
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

		public bool Equals(ITry<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasError && ErrorMessage!.Equals(other.ErrorMessage);
		}
	}
}