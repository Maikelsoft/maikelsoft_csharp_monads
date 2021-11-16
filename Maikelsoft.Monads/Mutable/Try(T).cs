﻿using System;
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

		public abstract TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue);

		public abstract void Match(Action<Exception> whenException, Action<T> whenValue);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> Select<TResult>(Func<T, TResult> selectorThatCanThrowException)
			=> Bind(value => Try.Create(() => selectorThatCanThrowException(value)));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOther"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <param name="selectorThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public Try<TResult> SelectMany<TOther, TResult>(Func<T, Try<TOther>> bind,
			Func<T, TOther, TResult> selectorThatCanThrowException)
			=> Bind(a => bind(a).Select(b => selectorThatCanThrowException(a, b)));
	}
}