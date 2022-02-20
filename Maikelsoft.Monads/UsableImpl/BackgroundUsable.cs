using System;
using System.Threading;
using System.Threading.Tasks;

namespace Maikelsoft.Monads.UsableImpl
{
	internal sealed class BackgroundUsable<T> : Usable<T>
		where T : notnull
	{
		private readonly Usable<T> _innerUsable;
		private readonly TaskCreationOptions _taskCreationOptions;
		private readonly TaskScheduler _taskScheduler;
		private readonly Action<Task> _onCompleted;

		public BackgroundUsable(Usable<T> innerUsable,
			TaskCreationOptions taskCreationOptions, TaskScheduler taskScheduler,
			Action<Task> onCompleted)
		{
			_innerUsable = innerUsable;
			_taskCreationOptions = taskCreationOptions;
			_taskScheduler = taskScheduler;
			_onCompleted = onCompleted;
		}

		public override void Use(Action<T> action)
		{
			Task.Factory
				.StartNew(() => _innerUsable.Use(action), CancellationToken.None, _taskCreationOptions, _taskScheduler)
				.ContinueWith(_onCompleted);
		}
	}
}