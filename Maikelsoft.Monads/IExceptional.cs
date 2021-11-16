using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	// TODO: Add stack trace / details property
	public interface IExceptional<T> : IEquatable<IExceptional<T>> 
		where T : IEquatable<T>
	{
		/// <summary>
		/// 
		/// </summary>
		T Value { get; }

		/// <summary>
		/// 
		/// </summary>
		bool HasError { get; }

		/// <summary>
		/// 
		/// </summary>
		bool HasValue { get; }

		/// <summary>
		/// 
		/// </summary>
		string? ErrorMessage { get; }

		/// <summary>
		/// Binds the wrapped value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="bind"></param>
		/// <returns></returns>
		[Pure]
		IExceptional<TResult> Bind<TResult>(Func<T, IExceptional<TResult>> bind) 
			where TResult : IEquatable<TResult>;

		TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue);

		void Match(Action<string> whenError, Action<T> whenValue);
	}
}