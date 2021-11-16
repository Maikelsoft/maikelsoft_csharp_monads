using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class DeferredUsable<T> : Usable<T>
		where T : notnull
	{
		private readonly Func<T> _create;
		private readonly Action<T> _cleanup;

		internal DeferredUsable(Func<T> create, Action<T> cleanup)
		{
			_create = create;
			_cleanup = cleanup;
		}

		public override void Use(Action<T> action)
		{
			T instance = _create();
			action(instance);
			_cleanup(instance);
		}

		public override TResult Use<TResult>(Func<T, TResult> func)
		{
			T instance = _create();
			TResult result = func(instance);
			_cleanup(instance);
			return result;
		}

		//public Usable<TResult> Bind<TResult>(Func<T, Usable<TResult>> bind)
		//	where TResult : notnull
		//{
		//	return new SelectUsable<T, TResult>(this, bind);
		//}
	}
}