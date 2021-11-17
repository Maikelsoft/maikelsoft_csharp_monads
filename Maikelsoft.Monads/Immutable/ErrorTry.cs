using System;

namespace Maikelsoft.Monads.Immutable
{

    internal sealed class ErrorTry<T> : Try<T>
		where T : IEquatable<T>
	{
		public override T Value => throw new InvalidOperationException("No value available.");
		public override bool HasError => true;
		public override bool HasValue => false;
		public override Error Error { get; }

		public ErrorTry(Error error)
		{
			Error = error;
		}

		public override Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind)
		{
			return new ErrorTry<TResult>(Error);
		}

		public override TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue)
		{
			return whenError(Error);
		}

		public override void Match(Action<Error> whenError, Action<T> whenValue)
		{
			whenError(Error);
		}

		public override int GetHashCode()
		{
			return Error!.GetHashCode();
		}

		public override bool Equals(Try<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.HasError && Error.Equals(other.Error);
		}
	}
}