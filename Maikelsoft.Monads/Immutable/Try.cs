﻿using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.Immutable
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
			where T : IEquatable<T>
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
		public static Try<T> FromValue<T>(T value) where T : IEquatable<T> =>
			new ValueTry<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromException<T>(Exception exception) 
			where T : IEquatable<T> =>
			new ErrorTry<T>(Error.FromException(exception));

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		[Pure]
		public static Try<T> FromError<T>(string errorMessage, string? details = null) 
			where T : IEquatable<T> =>
			new ErrorTry<T>(new Error(errorMessage, details));
	}
}