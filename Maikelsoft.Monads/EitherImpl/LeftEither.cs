using System;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.EitherImpl
{
    internal sealed class LeftEither<TLeft, TRight> : Either<TLeft, TRight>
        where TLeft : notnull
        where TRight : notnull
    {
        public override bool HasLeft => true;
        public override bool HasRight => false;
        internal override TLeft LeftValue { get; }
        internal override TRight RightValue => throw new InvalidOperationException();
        
        public override Either<TResult, TRight> BindLeft<TResult>(Func<TLeft, Either<TResult, TRight>> bind)
        {
            return bind(LeftValue);
        }

        public override Either<TLeft, TResult> BindRight<TResult>(Func<TRight, Either<TLeft, TResult>> bind)
        {
            return new LeftEither<TLeft, TResult>(LeftValue);
        }

        public override Task<Either<TResult, TRight>> BindLeftAsync<TResult>(Func<TLeft, Task<Either<TResult, TRight>>> bind)
        {
            return bind(LeftValue);
        }

        public override Task<Either<TLeft, TResult>> BindRightAsync<TResult>(Func<TRight, Task<Either<TLeft, TResult>>> bind)
        {
            Either<TLeft, TResult> result = new LeftEither<TLeft, TResult>(LeftValue);
            return Task.FromResult(result);
        }

        public override void WhenLeft(Action<TLeft> action)
        {
            action(LeftValue);
        }

        public override Task WhenLeftAsync(Func<TLeft, Task> func)
        {
            return func(LeftValue);
        }

        public override void WhenRight(Action<TRight> action)
        {
        }

        public override Task WhenRightAsync(Func<TRight, Task> func)
        {
            return Task.CompletedTask;
        }

        public override TLeft GetLeftOrDefault(TLeft defaultValue)
        {
            return LeftValue;
        }

        public override TRight GetRightOrDefault(TRight defaultValue)
        {
            return defaultValue;
        }

        public LeftEither(TLeft left)
        {
            LeftValue = left;
        }

        public override TResult Match<TResult>(Func<TLeft, TResult> whenLeft, Func<TRight, TResult> whenRight)
        {
            return whenLeft(LeftValue);
        }

        public override void Match(Action<TLeft> whenLeft, Action<TRight> whenRight)
        {
            whenLeft(LeftValue);
        }

        public override Task MatchAsync(Func<TLeft, Task> whenLeft, Func<TRight, Task> whenRight)
        {
            return whenLeft(LeftValue);
        }

        public override Task<TResult> MatchAsync<TResult>(Func<TLeft, Task<TResult>> whenLeft, Func<TRight, Task<TResult>> whenRight)
        {
            return whenLeft(LeftValue);
        }

        public override int GetHashCode()
        {
            return LeftValue.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is LeftEither<TLeft, TRight> other && LeftValue.Equals(other.LeftValue);
        }

        public override bool Equals(Either<TLeft, TRight>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is LeftEither<TLeft, TRight> either && LeftValue.Equals(either.LeftValue);
        }
    }
}