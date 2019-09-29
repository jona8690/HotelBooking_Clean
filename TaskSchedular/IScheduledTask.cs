using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedular
{
	public interface IScheduledTask
	{
		string Schedule { get; }

		Task ExecuteAsync(CancellationToken cancellationToken);
	}
}
