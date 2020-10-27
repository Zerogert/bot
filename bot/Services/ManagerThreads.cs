using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bot.Services
{
	public class ManagerThreads
	{
		private static bool _isLoading;
		private static int _counterTasks;

		private static int CounterTasks
		{
			get => _counterTasks;
			set
			{
				_counterTasks = value;
				IsLoading = CounterTasks != 0;
			}
		}

		public static bool IsLoading
		{
			get => _isLoading;
			private set
			{
				if (_isLoading == value)
					return;
				_isLoading = value;
				NotifyStaticPropertyChanged();
			}
		}

		public static Task StartNewTask(Action action, Action<Task> comeBack = null)
		{
			IsLoading = true;
			var newTask = Task.Factory.StartNew(action).ContinueWith(CompletedTask);
			if (comeBack != null)
			{
				newTask.ContinueWith(comeBack);
			}
			CounterTasks++;
			UpdateInterface(() => Keyboard.ClearFocus());
			return newTask;
		}


		public static void UpdateInterface(Action action)
		{
			Application.Current.Dispatcher.InvokeAsync(action).Wait();
		}

		public static event PropertyChangedEventHandler StaticPropertyChanged;

		private static void CompletedTask(Task task)
		{
			if (task.Status == TaskStatus.RanToCompletion) CounterTasks--;
		}

		private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
		}

	}
}
