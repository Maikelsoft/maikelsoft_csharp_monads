using System;

namespace Maikelsoft.Monads
{
	internal sealed class SelectManyUsable<TOuter, TInner, T> : IUsable<T>
		where TInner : notnull
		where TOuter : notnull
		where T : notnull
	{
		private readonly IUsable<TOuter> _outerUsable;
		private readonly Func<TOuter, IUsable<TInner>> _innerUsableSelector;
		private readonly Func<TOuter, TInner, T> _resultSelector;

		public SelectManyUsable(IUsable<TOuter> outerUsable, Func<TOuter, IUsable<TInner>> innerUsableSelector,
			Func<TOuter, TInner, T> resultSelector)
		{
			_outerUsable = outerUsable;
			_resultSelector = resultSelector;
			_innerUsableSelector = innerUsableSelector;
		}

		public void Use(Action<T> action)
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

		public TResult Use<TResult>(Func<T, TResult> func)
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