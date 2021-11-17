using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class SelectManyUsable<TOuter, TInner, T> : Usable<T>
		where TOuter : notnull
		where TInner : notnull
		where T : notnull
	{
		private readonly Usable<TOuter> _source;
		private readonly Func<TOuter, Usable<TInner>> _usableSelector;
		private readonly Func<TOuter, TInner, T> _resultSelector;

		public SelectManyUsable(Usable<TOuter> source, Func<TOuter, Usable<TInner>> usableSelector,
			Func<TOuter, TInner, T> resultSelector)
		{
			_source = source;
			_resultSelector = resultSelector;
			_usableSelector = usableSelector;
		}

		public override void Use(Action<T> action)
		{
			_source.Use(outerScope =>
			{
				_usableSelector(outerScope).Use(innerScope =>
				{
					T result = _resultSelector(outerScope, innerScope);
					action(result);
				});
			});
		}

		public override TResult Use<TResult>(Func<T, TResult> func)
		{
			return _source.Use(outerScope =>
			{
				return _usableSelector(outerScope).Use(innerScope =>
				{
					T value = _resultSelector(outerScope, innerScope);
					return func(value);
				});
			});
		}
	}
}