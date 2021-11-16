using System;

namespace Maikelsoft.Monads
{
	internal sealed class SelectUsable<T, TResult> : IUsable<TResult>
		where T : notnull
	{
		private readonly IUsable<T> _outerUsable;
		private readonly Func<T, TResult> _selector;

		public SelectUsable(IUsable<T> outerUsable, Func<T, TResult> selector)
		{
			_outerUsable = outerUsable;
			_selector = selector;
		}

		public void Use(Action<TResult> user)
		{
			_outerUsable.Use(obj =>
			{
				TResult result = _selector(obj);
				user(result);
			});
		}
	}
}