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
        public static IObservable<TLeft> LeftValues<TLeft, TRight>(this IObservable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(@try => @try.HasLeft).Select(either => either.Left);
        }

        [Pure]
        public static IObservable<TRight> RightValues<TLeft, TRight>(this IObservable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return source.Where(@try => @try.HasRight).Select(either => either.Right);
        }

        [Pure]
        public static IObservable<Either<TResult, TRight>> SelectLeft<TLeft, TRight, TResult>(
            this IObservable<Either<TLeft, TRight>> source, Func<TLeft, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.SelectLeft(selector));
        }

        [Pure]
        public static IObservable<Either<TLeft, TResult>> SelectRight<TLeft, TRight, TResult>(
            this IObservable<Either<TLeft, TRight>> source, Func<TRight, TResult> selector)
            where TLeft : notnull
            where TRight : notnull
            where TResult : notnull
        {
            return source.Select(@try => @try.SelectRight(selector));
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
        public static IObservable<Try<T>> Select<TSource, T>(
            this IObservable<Try<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(@try => @try.Select(selector));
        }

        [Pure]
        public static IObservable<Try<T>> TrySelect<TSource, T>(
            this IObservable<Try<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(@try => @try.TrySelect(selector));
        }

        //      [Pure]
        //public static IObservable<Try<TResult>> TryCombineLatest<TSource1, TSource2, TResult>(
        //	this IObservable<Try<TSource1>> source1,
        //	IObservable<Try<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
        //	where TSource1 : notnull
        //	where TSource2 : notnull
        //	where TResult : notnull
        //      {
        //          return source1.CombineLatest(source2, (try1, try2) => try1.CombineWith(try2, resultSelector));
        //      }

        //      [Pure]
        //public static IObservable<Try<TResult>> TryWithLatestFrom<TSource1, TSource2, TResult>(
        //	this IObservable<Try<TSource1>> source1,
        //	IObservable<Try<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
        //	where TSource1 : notnull
        //	where TSource2 : notnull
        //	where TResult : notnull
        //      {
        //          return source1.WithLatestFrom(source2, (try1, try2) => try1.CombineWith(try2, resultSelector));
        //      }

        //      [Pure]
        //public static IObservable<Try<TResult>> TryZip<TSource1, TSource2, TResult>(
        //	this IObservable<Try<TSource1>> source1,
        //	IObservable<Try<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
        //	where TSource1 : notnull
        //	where TSource2 : notnull
        //	where TResult : notnull
        //      {
        //          return source1.Zip(source2, (try1, try2) => try1.CombineWith(try2, resultSelector));
        //      }

        #endregion

        #region Optional Monad

        [Pure]
        public static IObservable<Optional<T>> OptionalSelect<TSource, T>(
            this IObservable<Optional<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(optional => optional.Select(selector));
        }

        [Pure]
        public static IObservable<Optional<TResult>> OptionalSelect<TSource, TResult>(
            this IObservable<Optional<TSource>> source, Func<TSource, int, TResult> selector)
            where TSource : notnull
            where TResult : notnull
        {
            return source.Select((optional, i) => optional.Select(value => selector(value, i)));
        }

        [Pure]
        public static IObservable<Optional<TResult>> OptionalCombineLatest<TSource1, TSource2, TResult>(
            this IObservable<Optional<TSource1>> source1,
            IObservable<Optional<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
            where TSource1 : notnull
            where TSource2 : notnull
            where TResult : notnull
        {
            return source1.CombineLatest(source2,
                (optional1, optional2) => optional1.CombineWith(optional2, resultSelector));
        }

        [Pure]
        public static IObservable<Optional<TResult>> OptionalWithLatestFrom<TSource1, TSource2, TResult>(
            this IObservable<Optional<TSource1>> source1,
            IObservable<Optional<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
            where TSource1 : notnull
            where TSource2 : notnull
            where TResult : notnull
        {
            return source1.WithLatestFrom(source2,
                (optional1, optional2) => optional1.CombineWith(optional2, resultSelector));
        }

        [Pure]
        public static IObservable<Optional<TResult>> OptionalZip<TSource1, TSource2, TResult>(
            this IObservable<Optional<TSource1>> source1,
            IObservable<Optional<TSource2>> source2, Func<TSource1, TSource2, TResult> resultSelector)
            where TSource1 : notnull
            where TSource2 : notnull
            where TResult : notnull
        {
            return source1.Zip(source2, (optional1, optional2) => optional1.CombineWith(optional2, resultSelector));
        }

        #endregion
    }
}