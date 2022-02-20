using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Maikelsoft.Monads.Linq
{
    /// <summary>
    /// Extends the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class ExtendIEnumerable
    {
        #region Either Monad
        
        [Pure]
        public static IEnumerable<TLeft> LeftValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(@try => @try.HasLeft).Select(either => either.Left);
        }

        [Pure]
        public static IEnumerable<TRight> RightValues<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(@try => @try.HasRight).Select(either => either.Right);
        }

        [Pure]
        public static IEnumerable<Either<TResult, TRight>> SelectLeft<TLeft, TRight, TResult>(
            this IEnumerable<Either<TLeft, TRight>> source, Func<TLeft, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.SelectLeft(selector));
        }

        [Pure]
        public static IEnumerable<Either<TLeft, TResult>> SelectRight<TLeft, TRight, TResult>(
            this IEnumerable<Either<TLeft, TRight>> source, Func<TRight, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.SelectRight(selector));
        }

        #endregion

        #region Try Monad

        [Pure]
        public static IEnumerable<T> Values<T>(this IEnumerable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.HasValue).Select(@try => @try.Value);
        }

        [Pure]
        public static IEnumerable<Error> Errors<T>(this IEnumerable<Try<T>> source)
            where T : notnull
        {
            return source.Where(@try => @try.HasError).Select(@try => @try.Error);
        }
        
        [Pure]
        public static IEnumerable<Try<TResult>> Select<TSource, TResult>(
            this IEnumerable<Try<TSource>> source, Func<TSource, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.Select(selector));
        }

        [Obsolete("Use Select override")]
        [Pure]
        public static IEnumerable<Try<TResult>> TrySelect<TSource, TResult>(
            this IEnumerable<Try<TSource>> source, Func<TSource, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.Select(selector));
        }

        public static void Match<T>(this IEnumerable<Try<T>> source, Action<Error> whenError, Action<T> whenValue)
            where T : notnull
        {
            foreach (Try<T> @try in source)
            {
                @try.Match(whenError, whenValue);
            }
        }

        public static IEnumerable<TResult> Match<T, TResult>(this IEnumerable<Try<T>> source, Func<Error, TResult> whenError,
            Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(@try => @try.Match(whenError, whenValue));
        }

        #endregion

        #region Optional Monad

        [Pure]
        public static IEnumerable<Optional<TResult>> OptionalSelect<TSource, TResult>(
            this IEnumerable<Optional<TSource>> source, Func<TSource, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select(optional => optional.Bind(value => Optional.From(selector(value))));
        }

        [Pure]
        public static IEnumerable<Optional<TResult>> OptionalSelect<TSource, TResult>(
            this IEnumerable<Optional<TSource>> source, Func<TSource, int, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select((optional, i) => optional.Bind(value => Optional.From(selector(value, i))));
        }

        public static void Match<T>(this IEnumerable<Optional<T>> source, Action whenEmpty, Action<T> whenValue)
            where T : notnull
        {
            foreach (Optional<T> optional in source)
            {
                optional.Match(whenEmpty, whenValue);
            }
        }

        public static IEnumerable<TResult> Match<T, TResult>(this IEnumerable<Optional<T>> source, Func<TResult> whenEmpty,
            Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(optional => optional.Match(whenEmpty, whenValue));
        }

        #endregion
    }
}