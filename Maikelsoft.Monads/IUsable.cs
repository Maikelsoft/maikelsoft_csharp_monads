using System;

namespace Maikelsoft.Monads
{
	public interface IUsable<out T>
		where T : notnull
	{
		public void Use(Action<T> user);
	}
}