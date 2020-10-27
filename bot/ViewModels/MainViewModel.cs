using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using bot.Helpers;
using bot.Services;

namespace bot.ViewModels
{
	public class MainViewModel:BaseViewModel
	{
		private BotService botService = new BotService("https://www.cyberforum.ru/log-in.php");
		private ICommand runCommand;
		private string logs;
		private string login;
		private string password;

		public ICommand RunCommand
		{
			get
			{
				runCommand = runCommand ?? new ActionCommand(Run);
				return runCommand;
			}
		}

		public string Logs
		{
			get => logs;
			set
			{
				if (logs == value)
					return;

				logs = value;
				RaisePropertyChanged("Logs");
			}
		}

		public string Login
		{
			get => login;
			set
			{
				if (login == value)
					return;

				login = value;
				RaisePropertyChanged("Login");
			}
		}

		public string Password
		{
			get => password;
			set
			{
				if (password == value)
					return;

				password = value;
				RaisePropertyChanged("Password");
			}
		}

		private void Run(object obj)
		{
			botService.SignIn(Login, Password);
			var test = botService.Web;
		}
	}
}
