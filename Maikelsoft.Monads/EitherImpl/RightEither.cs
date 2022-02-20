using System;

namespace Maikelsoft.Monads.EitherImpl
{
	internal sealed class RightEither<TLeft, TRight> : Either<TLeft, TRight>
		where TLeft : notnull
		where TRight : notnull
	{
        public override bool HasLeft => false;
        public override bool HasRight => true;
		public override TLeft Left => throw new InvalidOperationException();
        public override TRight Right { get; }

        public override Either<TResult, TRight> BindLeft<TResult>(Func<TLeft, Either<TResult, TRight>> bind)
        {
            return new RightEither<TResult, TRight>(Right);
        }

        public override Either<TLeft, TResult> BindRight<TResult>(Func<TRight, Either<TLeft, TResult>> bind)
        {
            return bind(Right);
        }

        public override TLeft GetLeftOrDefault(TLeft defaultValue) => defaultValue;

        public override TRight GetRightOrDefault(TRight defaultValue) => Right;

        public RightEither(TRight right)
		{
			Right = right;
		}

		public override TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight)
		{
			return whenRight(Right);
		}

		public override void Match(Action<TLeft> whenLeft, Action<TRight> whenRight)
		{
			whenRight(Right);
		}

		public override int GetHashCode()
		{
			return Right.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is RightEither<TLeft, TRight> other && Right.Equals(other.Right);
		}

		public override bool Equals(Either<TLeft, TRight>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other is RightEither<TLeft, TRight> && Right.Equals(other.Right);
		}
	}
}