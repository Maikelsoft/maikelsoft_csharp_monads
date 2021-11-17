using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Mutable
{
	public abstract class Try<T>
	{
		/// <summary>
		/// 
		/// </summary>
		public abstract bool HasValue { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract T Value { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract bool HasException { get; }

		/// <summary>
		/// 
		/// </summary>
		public abstract Exception? Exception { get; }

		/// <summary>
		/// Binds the wrapped value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		public abstract Try<TResult> Bind<TResult>(Func<T, Try<TResult>> bind);

		public void Deconstruct(out Exception? exception, out T value)
		{
			exception = Exception;
			value = Value;
		}

		public abstract TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue);

		public abstract void Match(Action<Exception> whenException, Action<T> whenValue);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> Select<TResult>(Func<T, TResult> selector)
			=> Bind(value => Try.Create(() => selector(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="trySelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> trySelector,
			Func<T, TOther, TResult> resultSelector) => 
			Bind(value1 => trySelector(value1).Select(value2 => resultSelector(value1, value2)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="other"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> CombineWith<TOther, TResult>(Try<TOther> other, Func<T, TOther, TResult> resultSelector) =>
			Bind(value1 => other.Select(value2 => resultSelector(value1, value2)));
	}
}