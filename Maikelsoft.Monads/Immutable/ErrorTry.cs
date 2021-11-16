using System;

namespace Maikelsoft.Monads.Immutable
{
	internal sealed class ErrorTry<T> : Try<T>
		where T : IEquatable<T>
	{
		public override T Value => throw new InvalidOperationException("No value available.");
		public override bool HasError => true;
		public override bool HasValue => false;
		public override string? ErrorMessage { get; }

		public ErrorTry(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
		{
			return new ErrorTry<TResult>(ErrorMessage!);
		}

		public override TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenError(ErrorMessage!);
		}

		public override void Match(Action<string> whenError, Action<T> whenValue)
		{
			whenError(ErrorMessage!);
		}

		public override int GetHashCode()
		{
			return ErrorMessage!.GetHashCode();
		}

		public override bool Equals(Try<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasError && ErrorMessage!.Equals(other.ErrorMessage);
		}
	}
}