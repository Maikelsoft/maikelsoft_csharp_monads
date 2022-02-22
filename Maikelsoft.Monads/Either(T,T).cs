using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.EitherImpl;

namespace Maikelsoft.Monads
{
    public abstract class Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
        where TLeft : notnull
        where TRight : notnull
    {
        public abstract bool HasLeft { get; }
        public abstract bool HasRight { get; }
        public abstract TLeft Left { get; }
        public abstract TRight Right { get; }

        #region Factory methods (internal)

        internal static Either<TLeft, TRight> FromLeft(TLeft left) => new LeftEither<TLeft, TRight>(left);

        internal static Either<TLeft, TRight> FromRight(TRight right) => new RightEither<TLeft, TRight>(right);

        #endregion

        #region Abstract methods

        [Pure]
        public abstract Either<TResult, TRight> BindLeft<TResult>(Func<TLeft, Either<TResult, TRight>> bind)
            where TResult : notnull;

        [Pure]
        public abstract Either<TLeft, TResult> BindRight<TResult>(Func<TRight, Either<TLeft, TResult>> bind)
            where TResult : notnull;

        public abstract TLeft GetLeftOrDefault(TLeft defaultValue);
        public abstract TRight GetRightOrDefault(TRight defaultValue);
        public abstract TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight);
        public abstract void Match(Action<TLeft> whenLeft, Action<TRight> whenRight);
        public abstract bool Equals(Either<TLeft, TRight>? other);

        #endregion

        [Pure]
        public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> selector) where TResult : notnull
        {
            return BindLeft(value =>
            {
                TResult result = selector(value);
                return Either<TResult, TRight>.FromLeft(result);
            });
        }

        [Pure]
        public Either<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> selector) where TResult : notnull
        {
            return BindRight(value =>
            {
                TResult result = selector(value);
                return Either<TLeft, TResult>.FromRight(result);
            });
        }
    }
}