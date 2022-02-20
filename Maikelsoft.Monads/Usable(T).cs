using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Maikelsoft.Monads.UsableImpl;

namespace Maikelsoft.Monads
{
	public abstract class Usable<T>
		where T : notnull
	{
		public abstract void Use(Action<T> action);

		[Pure]
		public Usable<T> UseInBackground() =>
			new BackgroundUsable<T>(this, TaskCreationOptions.None, TaskScheduler.Default, _ => { });

		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions) =>
			new BackgroundUsable<T>(this, taskCreationOptions, TaskScheduler.Default, _ => { });

		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler) =>
			new BackgroundUsable<T>(this, taskCreationOptions, taskScheduler, _ => { });

		[Pure]
		public Usable<T> UseInBackground(Action<Task> onCompleted) =>
			new BackgroundUsable<T>(this, TaskCreationOptions.None, TaskScheduler.Default, onCompleted);

		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions,
			Action<Task> onCompleted)
			=> new BackgroundUsable<T>(this, taskCreationOptions, TaskScheduler.Default, onCompleted);

		[Pure]
		public Usable<T> UseInBackground(TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler,
			Action<Task> onCompleted) => new BackgroundUsable<T>(this, taskCreationOptions, taskScheduler, onCompleted);
	}
}