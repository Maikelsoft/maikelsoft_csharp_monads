using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	public static class ExtendObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		[Pure]
		public static Optional<T> ToOptional<T>(this T value) where T : IEquatable<T> => new ValueOptional<T>(value);
	}
}