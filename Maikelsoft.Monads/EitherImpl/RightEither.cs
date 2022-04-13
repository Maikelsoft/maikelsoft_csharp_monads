using System;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.EitherImpl
{
	internal sealed class RightEither<TLeft, TRight> : Either<TLeft, TRight>
		where TLeft : notnull
		where TRight : notnull
    {
        public override bool HasLeft => false;
        public override bool HasRight => true;
        internal override TLeft LeftValue => throw new InvalidOperationException();
        internal override TRight RightValue { get; }
        
        public override Either<TResult, TRight> BindLeft<TResult>(Func<TLeft, Either<TResult, TRight>> bind)
        {
            return new RightEither<TResult, TRight>(RightValue);
        }

        public override Either<TLeft, TResult> BindRight<TResult>(Func<TRight, Either<TLeft, TResult>> bind)
        {
            return bind(RightValue);
        }

        public override Task<Either<TResult, TRight>> BindLeftAsync<TResult>(Func<TLeft, Task<Either<TResult, TRight>>> bind)
        {
            Either<TResult, TRight> result = new RightEither<TResult, TRight>(RightValue);
            return Task.FromResult(result);
        }

        public override Task<Either<TLeft, TResult>> BindRightAsync<TResult>(Func<TRight, Task<Either<TLeft, TResult>>> bind)
        {
            return bind(RightValue);
        }

        public override TLeft GetLeftOrDefault(TLeft defaultValue)
        {
            return defaultValue;
        }

        public override TRight GetRightOrDefault(TRight defaultValue)
        {
            return RightValue;
        }

        public RightEither(TRight right)
		{
			RightValue = right;
		}

		public override TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight)
		{
			return whenRight(RightValue);
		}

		public override void Match(Action<TLeft> whenLeft, Action<TRight> whenRight)
		{
			whenRight(RightValue);
		}

        public override Task MatchAsync(Func<TLeft, Task> whenLeft, Func<TRight, Task> whenRight)
        {
            return whenRight(RightValue);
        }

        public override Task<TResult> MatchAsync<TResult>(Func<TLeft, Task<TResult>> whenLeft, Func<TRight, Task<TResult>> whenRight)
        {
            return whenRight(RightValue);
        }

        public override int GetHashCode()
		{
			return RightValue.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is RightEither<TLeft, TRight> other && RightValue.Equals(other.RightValue);
		}

		public override bool Equals(Either<TLeft, TRight>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other is RightEither<TLeft, TRight> either && RightValue.Equals(either.RightValue);
		}
	}
}