using System;

namespace Maikelsoft.Monads
{
	internal sealed class SelectUsable<TOuter, T> : Usable<T>
		where TOuter : notnull
		where T : notnull
	{
		private readonly Usable<TOuter> _source;
		private readonly Func<TOuter, T> _resultSelector;

		public SelectUsable(Usable<TOuter> source, Func<TOuter, T> resultSelector)
		{
			_source = source;
			_resultSelector = resultSelector;
		}

		public override void Use(Action<T> action)
		{
			_source.Use(obj =>
			{
				T result = _resultSelector(obj);
				action(result);
			});
		}

		public override TResult Use<TResult>(Func<T, TResult> func)
		{
			return _source.Use(obj =>
			{
				T result = _resultSelector(obj);
				return func(result);
			});
		}
	}
}