using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads
{
	/// <summary>
	/// 
	/// </summary>
	public static class Exceptional
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="funcThatCanThrowException"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> Create<T>(Func<T> funcThatCanThrowException)
			where T : IEquatable<T>
		{
			try
			{
				T value = funcThatCanThrowException();
				return new ValueExceptional<T>(value);
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
		public static async Task<IExceptional<T>> Create<T>(Func<Task<T>> funcThatCanThrowException)
			where T : IEquatable<T>
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
		public static IExceptional<T> FromValue<T>(T value) where T : IEquatable<T> =>
			new ValueExceptional<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> FromException<T>(Exception exception) where T : IEquatable<T> =>
			new ErrorExceptional<T>(exception.Message);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> FromError<T>(string errorMessage) where T : IEquatable<T> =>
			new ErrorExceptional<T>(errorMessage);
	}
}