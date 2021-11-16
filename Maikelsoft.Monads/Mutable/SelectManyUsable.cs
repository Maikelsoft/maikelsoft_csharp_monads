using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class SelectManyUsable<TOuter, TInner, T> : Usable<T>
		where TOuter : notnull
		where TInner : notnull
		where T : notnull
	{
		private readonly Usable<TOuter> _outerUsable;
		private readonly Func<TOuter, Usable<TInner>> _innerUsableSelector;
		private readonly Func<TOuter, TInner, T> _resultSelector;

		public SelectManyUsable(Usable<TOuter> outerUsable, Func<TOuter, Usable<TInner>> innerUsableSelector,
			Func<TOuter, TInner, T> resultSelector)
		{
			_outerUsable = outerUsable;
			_resultSelector = resultSelector;
			_innerUsableSelector = innerUsableSelector;
		}

		public override void Use(Action<T> action)
		{
			_outerUsable.Use(outerScope =>
			{
				_innerUsableSelector(outerScope).Use(innerScope =>
				{
					T value = _resultSelector(outerScope, innerScope);
					action(value);
				});
			});
		}

		public override TResult Use<TResult>(Func<T, TResult> func)
		{
			return _outerUsable.Use(outerScope =>
			{
				return _innerUsableSelector(outerScope).Use(innerScope =>
				{
					T value = _resultSelector(outerScope, innerScope);
					return func(value);
				});
			});
		}
	}
}