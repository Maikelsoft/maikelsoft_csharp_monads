using System;

namespace Maikelsoft.Monads.EitherImpl
{
    internal sealed class LeftEither<TLeft, TRight> : Either<TLeft, TRight>
        where TLeft : notnull
        where TRight : notnull
    {
        public override bool HasLeft => true;
        public override bool HasRight => false;
        public override TLeft Left { get; }
        public override TRight Right => throw new InvalidOperationException();

        public override Either<TResult, TRight> BindLeft<TResult>(Func<TLeft, Either<TResult, TRight>> bind)
        {
            return bind(Left);
        }

        public override Either<TLeft, TResult> BindRight<TResult>(Func<TRight, Either<TLeft, TResult>> bind)
        {
            return new LeftEither<TLeft, TResult>(Left);
        }

        public override TLeft GetLeftOrDefault(TLeft defaultValue) => Left;

        public override TRight GetRightOrDefault(TRight defaultValue) => defaultValue;

        public LeftEither(TLeft left)
        {
            Left = left;
        }

        public override TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight)
        {
            return whenLeft(Left);
        }

        public override void Match(Action<TLeft> whenLeft, Action<TRight> whenRight)
        {
            whenLeft(Left);
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is LeftEither<TLeft, TRight> other && Left.Equals(other.Left);
        }

        public override bool Equals(Either<TLeft, TRight>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is LeftEither<TLeft, TRight> && Left.Equals(other.Left);
        }
    }
}