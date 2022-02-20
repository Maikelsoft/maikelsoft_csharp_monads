using System;
using System.Diagnostics.Contracts;
using Maikelsoft.Monads.UsableImpl;

namespace Maikelsoft.Monads
{
	public static class Usable
	{
		[Pure]
		public static Usable<T> Create<T>(Func<T> create, Action<T> cleanup)
			where T : notnull
		{
			return new DeferredUsable<T>(create, cleanup);
		}

		[Pure]
		public static Usable<T> Create<T>(Func<T> create)
			where T : IDisposable
		{
			return new DeferredUsable<T>(create, disposable => disposable.Dispose());
		}

		[Pure]
		public static Usable<Try<T>> Create<T>(Func<Try<T>> create)
			where T : IDisposable
		{
			return new DeferredUsable<Try<T>>(create, @try =>
			{
				if (@try.HasValue)
				{
					@try.Value.Dispose();
				}
			});
		}

		[Pure]
		public static Usable<T> Select<TOuter, T>(this Usable<TOuter> source, Func<TOuter, T> resultSelector)
			where TOuter : notnull
			where T : notnull =>
			new SelectUsable<TOuter, T>(source, resultSelector);

		[Pure]
		public static Usable<T> SelectMany<TOuter, TInner, T>(this Usable<TOuter> source, 
			Func<TOuter, Usable<TInner>> usableSelector,
			Func<TOuter, TInner, T> resultSelector)
			where TOuter : notnull
			where TInner : notnull
			where T : notnull =>
			new SelectManyUsable<TOuter,TInner,T>(source, usableSelector, resultSelector);
	}
}