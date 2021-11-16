using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Immutable
{
	public static class Optional
	{
		[Pure]
		public static Optional<T> From<T>(T value) where T : IEquatable<T> => Optional<T>.From(value);

		[Pure]
		public static Optional<T> Empty<T>() where T : IEquatable<T> => Optional<T>.Empty;
	}
}