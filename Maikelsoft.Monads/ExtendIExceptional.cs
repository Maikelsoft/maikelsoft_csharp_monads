using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public static class ExtendIExceptional
	{
		[Pure]
		public static IExceptional<TResult> Select<T, TResult>(this IExceptional<T> exceptional,
			Func<T, TResult> funcThatCanThrowException)
			where T : IEquatable<T>
			where TResult : IEquatable<TResult>
			=> exceptional.Bind(value => Exceptional.Create(() => funcThatCanThrowException(value)));

		[Pure]
		public static IExceptional<TResult> SelectMany<T, TOther, TResult>(this IExceptional<T> exceptional,
			Func<T, IExceptional<TOther>> bind, Func<T, TOther, TResult> project) 
			where T : IEquatable<T>
			where TOther : IEquatable<TOther>
			where TResult : IEquatable<TResult>
			=> exceptional.Bind(a => bind(a).Select(b => project(a, b)));
	}
}