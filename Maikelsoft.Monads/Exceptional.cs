using System;
using System.Diagnostics.Contracts;

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
		/// <param name="value"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> Value<T>(T value) where T : IEquatable<T> =>
			new ValueExceptional<T>(value);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="exception"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> Error<T>(Exception exception) where T : IEquatable<T> =>
			new ErrorExceptional<T>(exception.Message);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		[Pure]
		public static IExceptional<T> Error<T>(string errorMessage) where T : IEquatable<T> =>
			new ErrorExceptional<T>(errorMessage);
	}
}