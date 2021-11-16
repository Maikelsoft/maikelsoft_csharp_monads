using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public static class ExtendObject
	{
		[Pure]
		public static IExceptional<T> ToExceptional<T>(T value) where T : IEquatable<T> =>
			Exceptional.Value(value);
	}
}