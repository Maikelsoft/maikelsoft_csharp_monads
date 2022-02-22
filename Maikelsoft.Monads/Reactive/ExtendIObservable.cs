using System;
using System.Diagnostics.Contracts;
using System.Reactive.Linq;

namespace Maikelsoft.Monads.Reactive
{
    /// <summary>
    /// Extends the <see cref="IObservable{T}"/> interface.
    /// </summary>
    public static class ExtendIObservable
    {
        #region Either Monad

        [Pure]
        public static IObservable<TLeft> Lefts<TLeft, TRight>(this IObservable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(either => either.HasLeft).Select(either => either.Left);
        }

        [Pure]
        public static IObservable<TRight> Rights<TLeft, TRight>(this IObservable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(either => either.HasRight).Select(either => either.Right);
        }

        [Pure]
        public static IObservable<Either<TResult, TRight>> MapLefts<TLeft, TRight, TResult>(
            this IObservable<Either<TLeft, TRight>> source, Func<TLeft, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(either => either.MapLeft(selector));
        }

        [Pure]
        public static IObservable<Either<TLeft, TResult>> MapRights<TLeft, TRight, TResult>(
            this IObservable<Either<TLeft, TRight>> source, Func<TRight, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(either => either.MapRight(selector));
        }

        #endregion

        #region Try Monad

        [Pure]
        public static IObservable<T> Values<T>(this IObservable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.HasValue).Select(@try => @try.Value);
        }

        [Pure]
        public static IObservable<Error> Errors<T>(this IObservable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.HasError).Select(@try => @try.Error);
        }

        [Pure]
        public static IObservable<Try<T>> MapValues<TSource, T>(
            this IObservable<Try<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(@try => @try.Map(selector));
        }

        [Pure]
        public static IObservable<Try<T>> TryMapValues<TSource, T>(
            this IObservable<Try<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(@try => @try.TryMap(selector));
        }

        [Pure]
        public static IObservable<TResult> Match<T, TResult>(this IObservable<Try<T>> source,
            Func<Error, TResult> whenError, Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(@try => @try.Match(whenError, whenValue));
        }

        #endregion

        #region Optional Monad

        [Pure]
        public static IObservable<Optional<T>> MapValues<TSource, T>(
            this IObservable<Optional<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(optional => optional.Map(selector));
        }

        public static IObservable<TResult> Match<T, TResult>(this IObservable<Optional<T>> source,
            Func<TResult> whenEmpty,
            Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(optional => optional.Match(whenEmpty, whenValue));
        }

        #endregion
    }
}