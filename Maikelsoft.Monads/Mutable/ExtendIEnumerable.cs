using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Maikelsoft.Monads.Mutable
{
	/// <summary>
	/// Extends the <see cref="IEnumerable{T}"/> interface.
	/// </summary>
	public static class ExtendIEnumerable // TODO: Add LINQ methods for Usable Monad
	{
		#region Try Monad

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Try<TResult>> TrySelect<TSource, TResult>(
			this IEnumerable<Try<TSource>> source, Func<TSource, TResult> selector) =>
			source.Select(@try => @try.Bind(value => Try.Create(() => selector(value))));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Try<TResult>> TrySelect<TSource, TResult>(
			this IEnumerable<Try<TSource>> source, Func<TSource, int, TResult> selector) =>
			source.Select((@try, i) => @try.Bind(value => Try.Create(() => selector(value, i))));

		#endregion
	}
}