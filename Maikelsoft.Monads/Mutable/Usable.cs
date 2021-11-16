using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class Usable<T> : IUsable<T>
		where T : notnull
	{
		private readonly Func<T> _create;
		private readonly Action<T> _cleanup;

		internal Usable(Func<T> create, Action<T> cleanup)
		{
			_create = create;
			_cleanup = cleanup;
		}

		public void Use(Action<T> action)
		{
			T instance = _create();
			action(instance);
			_cleanup(instance);
		}

		public TResult Use<TResult>(Func<T, TResult> func)
		{
			T instance = _create();
			TResult result = func(instance);
			_cleanup(instance);
			return result;
		}
	}

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
		public static IUsable<T> Create<T>(Func<T> create, Action<T> cleanup)
			where T : notnull
		{
			return new Usable<T>(create, cleanup);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<T> Create<T>(Func<T> create)
			where T : IDisposable
		{
			return new Usable<T>(create, disposable => disposable.Dispose());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="create"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<ITry<T>> Create<T>(Func<ITry<T>> create)
			where T : IDisposable
		{
			return new Usable<ITry<T>>(create, x =>
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
		/// <param name="outerUsable"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<T> Select<TOuter, T>(this IUsable<TOuter> outerUsable, Func<TOuter, T> resultSelector)
			where TOuter : notnull
			where T : notnull
		{
			return new SelectUsable<TOuter, T>(outerUsable, resultSelector);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TOuter"></typeparam>
		/// <typeparam name="TInner"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="outerUsable"></param>
		/// <param name="innerUsableSelector"></param>
		/// <param name="resultSelector"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<T> SelectMany<TOuter, TInner, T>(this IUsable<TOuter> outerUsable, 
			Func<TOuter, IUsable<TInner>> innerUsableSelector,
			Func<TOuter, TInner, T> resultSelector)
			where TOuter : notnull
			where TInner : notnull
			where T : notnull
		{
			return new SelectManyUsable<TOuter,TInner,T>(outerUsable, innerUsableSelector, resultSelector);
		}
	}
}