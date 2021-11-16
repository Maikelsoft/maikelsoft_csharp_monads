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
		/// <param name="funcThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static ITry<TResult> Select<T, TResult>(this ITry<T> source,
			Func<T, TResult> funcThatCanThrowException)
			where T : IEquatable<T>
			where TResult : IEquatable<TResult>
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
			where T : IEquatable<T>
			where TOther : IEquatable<TOther>
			where TResult : IEquatable<TResult>
			=> source.Bind(a => bind(a).Select(b => project(a, b)));
	}
}