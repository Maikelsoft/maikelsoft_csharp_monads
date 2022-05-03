using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Maikelsoft.Monads.Linq
{
    /// <summary>
    /// Extends the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class ExtendIEnumerable
    {
        #region Either Monad
        
        [Pure]
        public static IEnumerable<TLeft> Lefts<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(either => either.HasLeft).Select(either => either.LeftValue);
        }

        [Pure]
        public static IEnumerable<TRight> Rights<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(either => either.HasRight).Select(either => either.RightValue);
        }

        [Pure]
        public static IEnumerable<Either<TResult, TRight>> MapLefts<TLeft, TRight, TResult>(
            this IEnumerable<Either<TLeft, TRight>> source, Func<TLeft, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(either => either.MapLeft(selector));
        }

        [Pure]
        public static IEnumerable<Either<TLeft, TResult>> MapRights<TLeft, TRight, TResult>(
            this IEnumerable<Either<TLeft, TRight>> source, Func<TRight, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(either => either.MapRight(selector));
        }

        #endregion

        #region Try Monad

        [Pure]
        public static IEnumerable<Error> Errors<T>(this IEnumerable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.Result.HasLeft).Select(@try => @try.Result.LeftValue);
        }

        [Pure]
        public static IEnumerable<T> Values<T>(this IEnumerable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.Result.HasRight).Select(@try => @try.Result.RightValue);
        }
        
        [Pure]
        public static IEnumerable<Try<T>> TryMap<TSource, T>(
            this IEnumerable<TSource> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(value => Try.Create(() => selector(value)));
        }

        [Pure]
        public static IEnumerable<Try<T>> TryMap<TSource, T>(
            this IEnumerable<Try<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(@try => @try.TryMap(selector));
        }
        
        [Pure]
        public static IEnumerable<TResult> Match<T, TResult>(this IEnumerable<Try<T>> source, Func<Error, TResult> whenError,
            Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(@try => @try.Match(whenError, whenValue));
        }

        public static void Match<T>(this IEnumerable<Try<T>> source, Action<Error> whenError, Action<T> whenValue)
            where T : notnull
        {
            foreach (Try<T> @try in source)
            {
                @try.Match(whenError, whenValue);
            }
        }

        #endregion

        #region Optional Monad

        [Pure]
        public static IEnumerable<Optional<TResult>> MapValues<TSource, TResult>(
            this IEnumerable<Optional<TSource>> source, Func<TSource, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select(optional => optional.Bind(value => Optional.FromValue(selector(value))));
        }

        public static IEnumerable<TResult> Match<T, TResult>(this IEnumerable<Optional<T>> source, Func<TResult> whenEmpty,
            Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(optional => optional.Match(whenEmpty, whenValue));
        }

        public static void Match<T>(this IEnumerable<Optional<T>> source, Action whenEmpty, Action<T> whenValue)
            where T : notnull
        {
            foreach (Optional<T> optional in source)
            {
                optional.Match(whenEmpty, whenValue);
            }
        }

        #endregion
    }
}