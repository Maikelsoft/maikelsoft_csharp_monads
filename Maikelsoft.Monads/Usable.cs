using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	internal sealed class Usable<T> : IUsable<T>
		where T : notnull
	{
		private readonly Func<T> _createInstance;
		private readonly Action<T> _destroyInstance;

		internal Usable(Func<T> createInstance, Action<T> destroyInstance)
		{
			_createInstance = createInstance;
			_destroyInstance = destroyInstance;
		}

		public void Use(Action<T> action)
		{
			T instance = _createInstance();
			action(instance);
			_destroyInstance(instance);
		}

		public TResult Use<TResult>(Func<T, TResult> func)
		{
			T instance = _createInstance();
			TResult result = func(instance);
			_destroyInstance(instance);
			return result;
		}
	}

	public static class Usable
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="createInstance"></param>
		/// <param name="destroyInstance"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<T> Create<T>(Func<T> createInstance, Action<T> destroyInstance)
			where T : notnull
		{
			return new Usable<T>(createInstance, destroyInstance);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="createInstance"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<T> Create<T>(Func<T> createInstance)
			where T : IDisposable
		{
			return new Usable<T>(createInstance, disposable => disposable.Dispose());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="usable"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		[Pure]
		public static IUsable<TResult> Select<T, TResult>(this IUsable<T> usable, Func<T, TResult> selector)
			where T : notnull
			where TResult : notnull
		{
			return new SelectUsable<T, TResult>(usable, selector);
		}
	}
}