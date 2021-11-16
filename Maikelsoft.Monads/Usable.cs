using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
	public sealed class Usable<T> : IUsable<T>
		where T : notnull
	{
		private readonly Func<T> _createInstance;
		private readonly Action<T> _destroyInstance;

		internal Usable(Func<T> createInstance, Action<T> destroyInstance)
		{
			_createInstance = createInstance;
			_destroyInstance = destroyInstance;
		}

		public void Use(Action<T> user)
		{
			T instance = _createInstance();
			user(instance);
			_destroyInstance(instance);
		}
	}

	public static class Usable
	{
		[Pure]
		public static IUsable<T> Create<T>(Func<T> createInstance, Action<T> destroyInstance)
			where T : notnull
		{
			return new Usable<T>(createInstance, destroyInstance);
		}

		[Pure]
		public static IUsable<T> Create<T>(Func<T> createInstance)
			where T : notnull, IDisposable
		{
			return new Usable<T>(createInstance, disposable => disposable.Dispose());
		}

		[Pure]
		public static IUsable<TResult> Select<T, TResult>(this IUsable<T> usable, Func<T, TResult> selector)
		{
			return new SelectUsable<T, TResult>(usable, selector);
		}
	}
}