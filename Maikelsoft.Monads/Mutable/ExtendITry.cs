using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Mutable
{
	public static class ExtendITry
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static Try<TResult> TrySelect<T, TResult>(this Try<T> source,
			Func<T, TResult> selectorThatCanThrowException)
			=> source.Bind(value => Try.Create(() => selectorThatCanThrowException(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="bind"></param>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static Try<TResult> TrySelectMany<T, TOther, TResult>(this Try<T> source,
			Func<T, Try<TOther>> bind, Func<T, TOther, TResult> selectorThatCanThrowException)
			=> source.Bind(a => bind(a).TrySelect(b => selectorThatCanThrowException(a, b)));
	}
}