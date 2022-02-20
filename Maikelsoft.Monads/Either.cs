using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public static class Either
    {
        [Pure]
        public static Either<TLeft, TRight> FromLeft<TLeft, TRight>(TLeft left) where TLeft : notnull
            where TRight : notnull
        {
            return Either<TLeft, TRight>.FromLeft(left);
        }

        [Pure]
        public static Either<TLeft, TRight> FromRight<TLeft, TRight>(TRight right) where TLeft : notnull
            where TRight : notnull
        {
            return Either<TLeft, TRight>.FromRight(right);
        }
    }
}