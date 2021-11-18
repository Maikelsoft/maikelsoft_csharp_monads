using System;

namespace Maikelsoft.Monads
{
	public abstract class Usable<T>
		where T : notnull
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="action"></param>
		public abstract void Use(Action<T> action);
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		public abstract TResult Use<TResult>(Func<T, TResult> func);
	}
}