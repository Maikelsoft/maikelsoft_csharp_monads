using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Maikelsoft.Monads.UsableImpl;

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
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground() =>
			new BackgroundUsable<T>(this, TaskCreationOptions.None, TaskScheduler.Default, _ => { });

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="taskCreationOptions"></param>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions) =>
			new BackgroundUsable<T>(this, taskCreationOptions, TaskScheduler.Default, _ => { });

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="taskCreationOptions"></param>
		/// <param name="taskScheduler"></param>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler) =>
			new BackgroundUsable<T>(this, taskCreationOptions, taskScheduler, _ => { });

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="onCompleted"></param>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground(Action<Task> onCompleted) =>
			new BackgroundUsable<T>(this, TaskCreationOptions.None, TaskScheduler.Default, onCompleted);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="taskCreationOptions"></param>
		/// <param name="onCompleted"></param>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions,
			Action<Task> onCompleted)
			=> new BackgroundUsable<T>(this, taskCreationOptions, TaskScheduler.Default, onCompleted);

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="taskCreationOptions"></param>
		/// <param name="taskScheduler"></param>
		/// <param name="onCompleted"></param>
		/// <returns></returns>
		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler,
			Action<Task> onCompleted) => new BackgroundUsable<T>(this, taskCreationOptions, taskScheduler, onCompleted);
	}
}