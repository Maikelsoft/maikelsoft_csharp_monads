using System;

namespace Maikelsoft.Monads.Mutable
{
	internal sealed class SelectUsable<TOuter, T> : IUsable<T>
		where TOuter : notnull
		where T : notnull
	{
		private readonly IUsable<TOuter> _outerUsable;
		private readonly Func<TOuter, T> _resultSelector;

		public SelectUsable(IUsable<TOuter> outerUsable, Func<TOuter, T> resultSelector)
		{
			_outerUsable = outerUsable;
			_resultSelector = resultSelector;
		}

		public void Use(Action<T> action)
		{
			_outerUsable.Use(obj =>
			{
				T result = _resultSelector(obj);
				action(result);
			});
		}

		public TResult Use<TResult>(Func<T, TResult> func)
		{
			return _outerUsable.Use(obj =>
			{
				T result = _resultSelector(obj);
				return func(result);
			});
		}
	}
}