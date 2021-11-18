using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public static class Optional
	{
		[Pure]
		public static Optional<T> From<T>(T value) where T : notnull => Optional<T>.From(value);

		[Pure]
		public static Optional<T> Empty<T>() where T : notnull => Optional<T>.Empty;
	}
}