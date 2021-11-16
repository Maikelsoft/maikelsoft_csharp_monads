using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
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
		public static ITry<TResult> TrySelect<T, TResult>(this ITry<T> source,
			Func<T, TResult> selectorThatCanThrowException)
			where T : IEquatable<T>
			where TResult : IEquatable<TResult>
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
		public static ITry<TResult> TrySelectMany<T, TOther, TResult>(this ITry<T> source,
			Func<T, ITry<TOther>> bind, Func<T, TOther, TResult> selectorThatCanThrowException) 
			where T : IEquatable<T>
			where TOther : IEquatable<TOther>
			where TResult : IEquatable<TResult>
			=> source.Bind(a => bind(a).TrySelect(b => selectorThatCanThrowException(a, b)));
	}
}