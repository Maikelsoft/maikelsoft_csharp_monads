using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Maikelsoft.Monads.EitherImpl;

namespace Maikelsoft.Monads
{
    public abstract class Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
        where TLeft : notnull
        where TRight : notnull
    {
        public abstract bool HasLeft { get; }
        public abstract bool HasRight { get; }
        internal abstract TLeft LeftValue { get; }
        internal abstract TRight RightValue { get; }
        
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

        [Pure]
        public abstract Task<Either<TResult, TRight>> BindLeftAsync<TResult>(Func<TLeft, Task<Either<TResult, TRight>>> bind)
            where TResult : notnull;

        [Pure]
        public abstract Task<Either<TLeft, TResult>> BindRightAsync<TResult>(Func<TRight, Task<Either<TLeft, TResult>>> bind)
            where TResult : notnull;

        public abstract TLeft GetLeftOrDefault(TLeft defaultValue);
        public abstract TRight GetRightOrDefault(TRight defaultValue);
        public abstract TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight);
        public abstract void Match(Action<TLeft> whenLeft, Action<TRight> whenRight);
        public abstract Task MatchAsync(Func<TLeft, Task> whenLeft, Func<TRight, Task> whenRight);
        public abstract Task<TResult> MatchAsync<TResult>(Func<TLeft, Task<TResult>> whenLeft, Func<TRight, Task<TResult>> whenRight);
        public abstract bool Equals(Either<TLeft, TRight>? other);

        #endregion

        [Pure]
        public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> selector) where TResult : notnull
        {
            return BindLeft(left =>
            {
                TResult result = selector(left);
                return Either<TResult, TRight>.FromLeft(result);
            });
        }

        [Pure]
        public Either<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> selector) where TResult : notnull
        {
            return BindRight(right =>
            {
                TResult result = selector(right);
                return Either<TLeft, TResult>.FromRight(result);
            });
        }

        [Pure]
        public Task<Either<TResult, TRight>> MapLeftAsync<TResult>(Func<TLeft, Task<TResult>> selector) where TResult : notnull
        {
            return BindLeftAsync(async left =>
            {
                TResult result = await selector(left);
                return Either<TResult, TRight>.FromLeft(result);
            });
        }

        [Pure]
        public Task<Either<TLeft, TResult>> MapRightAsync<TResult>(Func<TRight, Task<TResult>> selector) where TResult : notnull
        {
            return BindRightAsync(async right =>
            {
                TResult result = await selector(right);
                return Either<TLeft, TResult>.FromRight(result);
            });
        }
    }
}