using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	// TODO: Add stack trace / details property
	public interface ITry<T> : IEquatable<ITry<T>> 
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
		ITry<TResult> Bind<TResult>(Func<T, ITry<TResult>> bind) 
			where TResult : IEquatable<TResult>;

		TResult Match<TResult>(Func<string, TResult> whenError, Func<T, TResult> whenValue)
			where TResult : IEquatable<TResult>;

		void Match(Action<string> whenError, Action<T> whenValue);
	}
}