using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
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
		public static Optional<T> ToOptional<T>(this T value) where T : notnull => new ValueOptional<T>(value);
	}
}