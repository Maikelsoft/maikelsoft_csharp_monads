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
		/// <param name="funcThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static ITry<TResult> Select<T, TResult>(this ITry<T> source,
			Func<T, TResult> funcThatCanThrowException)
			=> source.Bind(value => Try.Create(() => funcThatCanThrowException(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="source"></param>
		/// <param name="bind"></param>
		/// <param name="project"></param>
		/// <returns></returns>
		[Pure]
		public static ITry<TResult> SelectMany<T, TOther, TResult>(this ITry<T> source,
			Func<T, ITry<TOther>> bind, Func<T, TOther, TResult> project)
			=> source.Bind(a => bind(a).Select(b => project(a, b)));
	}
}