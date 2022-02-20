using System.Diagnostics.Contracts;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
	public static class ExtendObject
	{
		[Pure]
		public static Optional<T> ToOptional<T>(this T value) where T : notnull => new ValueOptional<T>(value);

		[Pure]
        public static Try<T> ToTry<T>(this T value) where T : notnull => Try.FromValue(value);
	}
}