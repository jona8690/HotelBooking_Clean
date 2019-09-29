using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedular
{
	public class SchedulerHostedService : IHostedService
	{

		private Task _executingTask;
		private CancellationTokenSource _cts;

		public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

		private readonly List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();
		public SchedulerHostedService(IEnumerable<IScheduledTask> scheduledTasks)
		{
			var referenceTime = DateTime.UtcNow;

			foreach (var scheduledTask in scheduledTasks)
			{
				_scheduledTasks.Add(new SchedulerTaskWrapper
				{
					Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
					Task = scheduledTask,
					NextRunTime = referenceTime
				});
			}
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			// Create a linked token so we can trigger cancellation outside of this token's cancellation
			_cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

			// Store the task we're executing
			_executingTask = ExecuteAsync(_cts.Token);

			// If the task is completed then return it, otherwise it's running
			return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			// Stop called without start
			if (_executingTask == null)
			{
				return;
			}

			// Signal cancellation to the executing method
			_cts.Cancel();

			// Wait until the task completes or the stop token triggers
			await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

			// Throw if cancellation triggered
			cancellationToken.ThrowIfCancellationRequested();
		}

		protected async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await ExecuteOnceAsync(cancellationToken);

				await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
			}
		}

		private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
		{
			var taskFactory = new TaskFactory(TaskScheduler.Current);
			var referenceTime = DateTime.UtcNow;

			var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();

			foreach (var taskThatShouldRun in tasksThatShouldRun)
			{
				taskThatShouldRun.Increment();

				await taskFactory.StartNew(
					async () =>
					{
						try
						{
							await taskThatShouldRun.Task.ExecuteAsync(cancellationToken);
						}
						catch (Exception ex)
						{
							var args = new UnobservedTaskExceptionEventArgs(
								ex as AggregateException ?? new AggregateException(ex));

							UnobservedTaskException?.Invoke(this, args);

							if (!args.Observed)
							{
								throw;
							}
						}
					},
					cancellationToken);
			}
		}
	}
}
