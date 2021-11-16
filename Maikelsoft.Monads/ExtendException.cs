using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public static class ExtendException
	{
		[Pure]
		public static IExceptional<T> ToExceptional<T>(Exception exception) where T : IEquatable<T> =>
			Exceptional.Error<T>(exception);
	}
}