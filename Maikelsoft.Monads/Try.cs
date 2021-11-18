using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
	/// <summary>
	/// 
	/// </summary>
	public static class Try
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> Create<T>(Func<T> func)
			where T : notnull
		{
			try
			{
				T value = func();
				return new ValueTry<T>(value);
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
			{
				return FromException<T>(exception);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		[Pure]
		public static async Task<Try<T>> Create<T>(Func<Task<T>> func)
			where T : notnull
		{
			try
			{
				return FromValue(await func().ConfigureAwait(false));
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
			{
				return FromException<T>(exception);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromValue<T>(T value) where T : notnull => new ValueTry<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromException<T>(Exception exception) where T : notnull =>
			new ErrorTry<T>(Error.FromException(exception));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="errorMessage"></param>
		/// <param name="details"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromError<T>(string errorMessage, string? details = null) where T : notnull
			=> new ErrorTry<T>(new Error(errorMessage, details));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="error"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromError<T>(Error error) where T : notnull => new ErrorTry<T>(error);
	}
}