using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Mutable
{
	public static class Usable
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <param name="cleanup"></param>
		/// <returns></returns>
		[Pure]
		public static Usable<T> Create<T>(Func<T> create, Action<T> cleanup)
			where T : notnull
		{
			return new DeferredUsable<T>(create, cleanup);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <returns></returns>
		[Pure]
		public static Usable<T> Create<T>(Func<T> create)
			where T : IDisposable
		{
			return new DeferredUsable<T>(create, disposable => disposable.Dispose());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <returns></returns>
		[Pure]
		public static Usable<Try<T>> Create<T>(Func<Try<T>> create)
			where T : IDisposable
		{
			return new DeferredUsable<Try<T>>(create, x =>
			{
				if (x.HasValue)
				{
					x.Value.Dispose();
				}
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOuter"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public static Usable<T> Select<TOuter, T>(this Usable<TOuter> source, Func<TOuter, T> resultSelector)
			where TOuter : notnull
			where T : notnull =>
			new SelectUsable<TOuter, T>(source, resultSelector);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOuter"></typeparam>
		/// <typeparam name="TInner"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="usableSelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
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