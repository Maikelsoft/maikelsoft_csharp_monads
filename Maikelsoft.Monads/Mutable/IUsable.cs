using System;

namespace Maikelsoft.Monads.Mutable
{
	public interface IUsable<out T>
		where T : notnull
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="action"></param>
		public void Use(Action<T> action);
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		public TResult Use<TResult>(Func<T, TResult> func);
	}
}