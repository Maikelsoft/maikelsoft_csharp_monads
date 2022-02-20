using System;

namespace Maikelsoft.Monads.Reactive
{
	public static class ExtendIObserver
	{
        #region Either Monad

        public static void OnNextLeft<TLeft, TRight>(this IObserver<Either<TLeft, TRight>> observer, TLeft value)
            where TLeft: notnull
            where TRight: notnull
        {
            observer.OnNext(Either<TLeft, TRight>.FromLeft(value));
        }

        public static void OnNextRight<TLeft, TRight>(this IObserver<Either<TLeft, TRight>> observer, TRight value)
            where TLeft: notnull
            where TRight: notnull
        {
            observer.OnNext(Either<TLeft, TRight>.FromRight(value));
        }

        #endregion

        #region Try Monad

        public static void OnNextError<T>(this IObserver<Try<T>> observer, string errorMessage, string? details = null)
            where T: notnull
        {
            observer.OnNext(Try.FromError<T>(errorMessage, details));
        }

        public static void OnNextError<T>(this IObserver<Try<T>> observer, Error error)
            where T: notnull
        {
            observer.OnNext(Try.FromError<T>(error));
        }

        public static void OnNextException<T>(this IObserver<Try<T>> observer, Exception exception)
            where T: notnull
        {
            observer.OnNext(Try.FromException<T>(exception));
        }

        public static void OnNextValue<T>(this IObserver<Try<T>> observer, T value)
            where T: notnull
        {
            observer.OnNext(Try.FromValue(value));
        }

        #endregion
    }
}