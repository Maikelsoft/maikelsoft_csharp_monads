﻿using System;
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
            return Observable.Create<TLeft>(observer =>
            {
                void OnNext(Either<TLeft, TRight> @try)
                {
                    @try.WhenLeft(observer.OnNext);
                }

                return source.Subscribe(OnNext, observer.OnError, observer.OnCompleted);
            });
        }

        [Pure]
        public static IObservable<TRight> Rights<TLeft, TRight>(this IObservable<Either<TLeft, TRight>> source)
            where TLeft : notnull
            where TRight : notnull
        {
            return Observable.Create<TRight>(observer =>
            {
                void OnNext(Either<TLeft, TRight> @try)
                {
                    @try.WhenRight(observer.OnNext);
                }

                return source.Subscribe(OnNext, observer.OnError, observer.OnCompleted);
            });
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
        public static IObservable<Error> Errors<T>(this IObservable<Try<T>> source)
            where T : notnull
        {
            return Observable.Create<Error>(observer =>
            {
                void OnNext(Try<T> @try)
                {
                    @try.WhenError(observer.OnNext);
                }

                return source.Subscribe(OnNext, observer.OnError, observer.OnCompleted);
            });
        }

        [Pure]
        public static IObservable<T> Values<T>(this IObservable<Try<T>> source)
            where T : notnull
        {
            return Observable.Create<T>(observer =>
            {
                void OnNext(Try<T> @try)
                {
                    @try.WhenValue(observer.OnNext);
                }

                return source.Subscribe(OnNext, observer.OnError, observer.OnCompleted);
            });
        }

        [Pure]
        public static IObservable<Try<T>> TryMap<TSource, T>(
            this IObservable<TSource> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(value => Try.Create(() => selector(value)));
        }

        [Pure]
        public static IObservable<Try<T>> TryMap<TSource, T>(
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
        public static IObservable<T> Values<T>(this IObservable<Optional<T>> source)
            where T : notnull
        {
            return Observable.Create<T>(observer =>
            {
                void OnNext(Optional<T> optional)
                {
                    optional.WhenValue(observer.OnNext);
                }

                return source.Subscribe(OnNext, observer.OnError, observer.OnCompleted);
            });
        }

        [Pure]
        public static IObservable<Optional<T>> MapValues<TSource, T>(
            this IObservable<Optional<TSource>> source, Func<TSource, T> selector)
            where TSource : notnull
            where T : notnull
        {
            return source.Select(optional => optional.Map(selector));
        }

        public static IObservable<TResult> Match<T, TResult>(this IObservable<Optional<T>> source,
            Func<TResult> whenEmpty, Func<T, TResult> whenValue)
            where T : notnull
        {
            return source.Select(optional => optional.Match(whenEmpty, whenValue));
        }

        #endregion
    }
}