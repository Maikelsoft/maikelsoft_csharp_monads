using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Maikelsoft.Monads.Mutable
{
	/// <summary>
	/// Extends the <see cref="IEnumerable{T}"/> interface.
	/// </summary>
	public static class ExtendIEnumerable
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Try<T>> Select<TSource, T>(
			this IEnumerable<Try<TSource>> source, Func<TSource, T> selector) =>
			source.Select(@try => @try.Select(selector));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Try<T>> TrySelect<TSource, T>(this IEnumerable<TSource> source,
			Func<TSource, T> selector)
			=> source.Select(value => Try.Create(() => selector(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Try<T>> Bind<TSource, T>(
			this IEnumerable<Try<TSource>> source, Func<TSource, Try<T>> selector) =>
			source.Select(@try => @try.Bind(selector));
	}
}