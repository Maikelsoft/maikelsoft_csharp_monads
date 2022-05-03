using System;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.OptionalImpl
{
    internal sealed class EmptyOptional<T> : Optional<T>
        where T : notnull
    {
        private static EmptyOptional<T>? _instance;

        public static EmptyOptional<T> Instance => _instance ??= new EmptyOptional<T>();

        [Obsolete]
        public override T Value => throw new InvalidOperationException();
        
        [Obsolete]
        public override bool HasValue => false;

        public override int GetHashCode() => typeof(T).GetHashCode();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is EmptyOptional<T>;
        }

        public override bool Equals(Optional<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is EmptyOptional<T>;
        }

        public override T GetValueOrDefault(T defaultValue)
        {
            return defaultValue;
        }

        public override void WhenValue(Action<T> action)
        {
        }

        public override Task WhenValue(Func<T, Task> func)
        {
            return Task.CompletedTask;
        }

        public override void Match(Action whenEmpty, Action<T> whenValue)
        {
            whenEmpty();
        }

        public override Task Match(Func<Task> whenEmpty, Func<T, Task> whenValue)
        {
            return whenEmpty();
        }

        public override TResult Match<TResult>(Func<TResult> whenEmpty, Func<T, TResult> whenValue)
        {
            return whenEmpty();
        }

        public override Task<TResult> Match<TResult>(Func<Task<TResult>> whenEmpty, Func<T, Task<TResult>> whenValue)
        {
            return whenEmpty();
        }

        public override Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> bind)
        {
            return EmptyOptional<TResult>.Instance;
        }
    }
}