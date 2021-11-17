using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Maikelsoft.Monads.Immutable
{
	/// <summary>
	/// Extends the <see cref="IEnumerable{T}"/> interface.
	/// </summary>
	public static class ExtendIEnumerable
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
			this IEnumerable<Try<TSource>> source, Func<TSource, TResult> selector)
			where TSource : IEquatable<TSource>
			where TResult : IEquatable<TResult> =>
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
			this IEnumerable<Try<TSource>> source, Func<TSource, int, TResult> selector)
			where TSource : IEquatable<TSource>
			where TResult : IEquatable<TResult> =>
			source.Select((@try, i) => @try.Bind(value => Try.Create(() => selector(value, i))));

		#endregion

		#region Optional Monad

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Optional<TResult>> OptionalSelect<TSource, TResult>(
			this IEnumerable<Optional<TSource>> source, Func<TSource, TResult> selector)
			where TSource : IEquatable<TSource>
			where TResult : IEquatable<TResult> =>
			source.Select(optional => optional.Bind(value => Optional.From(selector(value))));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IEnumerable<Optional<TResult>> OptionalSelect<TSource, TResult>(
			this IEnumerable<Optional<TSource>> source, Func<TSource, int, TResult> selector)
			where TSource : IEquatable<TSource>
			where TResult : IEquatable<TResult> =>
			source.Select((optional, i) => optional.Bind(value => Optional.From(selector(value, i))));

		#endregion
	}
}