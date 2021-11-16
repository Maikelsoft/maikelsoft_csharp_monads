using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Mutable
{
	public interface ITry<out T>
	{
		/// <summary>
		/// 
		/// </summary>
		T Value { get; }

		/// <summary>
		/// 
		/// </summary>
		bool HasException { get; }

		/// <summary>
		/// 
		/// </summary>
		bool HasValue { get; }

		/// <summary>
		/// 
		/// </summary>
		Exception? Exception { get; }

		/// <summary>
		/// Binds the wrapped value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind);

		TResult Match<TResult>(Func<Exception, TResult> whenException, Func<T, TResult> whenValue);

		void Match(Action<Exception> whenException, Action<T> whenValue);
	}
}