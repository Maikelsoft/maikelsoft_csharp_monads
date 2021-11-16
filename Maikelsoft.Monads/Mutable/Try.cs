using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.Mutable
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
		/// <param name="funcThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> Create<T>(Func<T> funcThatCanThrowException)
		{
			try
			{
				T value = funcThatCanThrowException();
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
		/// <param name="funcThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static async Task<Try<T>> Create<T>(Func<Task<T>> funcThatCanThrowException)
		{
			try
			{
				return FromValue(await funcThatCanThrowException().ConfigureAwait(false));
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
		public static Try<T> FromValue<T>(T value) => new ValueTry<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromException<T>(Exception exception) => new ExceptionTry<T>(exception);
	}
}