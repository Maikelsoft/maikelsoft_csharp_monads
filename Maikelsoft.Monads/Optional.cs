using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.OptionalImpl;

namespace Maikelsoft.Monads
{
	public static class Optional
	{
        [Pure]
        public static Optional<T> FromValue<T>(T value) where T : notnull => new ValueOptional<T>(value);

		[Obsolete("Use FromValue<T>()")]
		[Pure]
		public static Optional<T> From<T>(T value) where T : notnull => new ValueOptional<T>(value);

		[Obsolete("Use Optional<T>.Empty")]
		[Pure]
		public static Optional<T> Empty<T>() where T : notnull => EmptyOptional<T>.Instance;
	}
}