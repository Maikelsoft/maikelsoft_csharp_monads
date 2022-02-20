﻿using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public sealed class Try2<T> : IEquatable<Try2<T>>, IOptional<T>
        where T : notnull
    {
        public Either<Error, T> Result { get; }
        public bool HasError => Result.HasLeft;
        public bool HasValue => Result.HasRight;
        public Error Error => Result.Left;
        public T Value => Result.Right;
        
        internal Try2(Either<Error, T> result)
        {
            Result = result;
        }

        [Pure]
        public Try2<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull
        {
            Either<Error, TResult> result = Result.BindRight(value => Either.FromRight<Error, TResult>(selector(value)));
            return new Try2<TResult>(result);
        }

        public TResult Match<TResult>(Func<Error, TResult> whenError, Func<T, TResult> whenValue) => 
            Result.Match(whenError, whenValue);

        public void Match(Action<Error> whenError, Action<T> whenValue) => 
            Result.Match(whenError, whenValue);

        public T GetValueOrDefault(T defaultValue) => Result.GetRightOrDefault(defaultValue);

        #region Equality members

        public bool Equals(Try2<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Result.Equals(other.Result);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Try2<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Result.GetHashCode();
        }

        #endregion
    }
}