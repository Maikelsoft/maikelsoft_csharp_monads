using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
	// This class should not reference the implementation classes.
	public static class Try
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> Create<T>(Func<T> func) where T : notnull => Try<T>.Create(func);

        /// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		[Pure]
		public static Task<Try<T>> Create<T>(Func<Task<T>> func) where T : notnull => Try<T>.Create(func);

        /// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromValue<T>(T value) where T : notnull => Try<T>.FromValue(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromException<T>(Exception exception) where T : notnull => Try<T>.FromException(exception);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="errorMessage"></param>
		/// <param name="details"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromError<T>(string errorMessage, string? details = null) where T : notnull
			=> Try<T>.FromError(errorMessage, details);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="error"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromError<T>(Error error) where T : notnull => Try<T>.FromError(error);
	}
}