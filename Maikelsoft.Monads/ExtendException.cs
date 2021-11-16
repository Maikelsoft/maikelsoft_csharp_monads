using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public static class ExtendException
	{
		[Pure]
		public static IExceptional<T> ToExceptional<T>(this Exception exception) where T : IEquatable<T> =>
			Exceptional.FromException<T>(exception);
	}
}