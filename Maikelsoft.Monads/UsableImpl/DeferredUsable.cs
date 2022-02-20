using System;

namespace Maikelsoft.Monads.UsableImpl
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
	}
}