using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace bot.Services
{
	public class BotService
	{
		private IWebDriver web = new ChromeDriver();

		public IWebDriver Web => web;

		public BotService(string url)
		{
			web.Navigate().GoToUrl(url);
		}
		
		public void SignIn(string login, string password)
		{

			var loginField = web.FindElement(By.XPath("/html/body/div/div[1]/div/div/table[2]/tbody/tr[2]/td/div/div/form/table/tbody/tr[1]/td/input"));
			var passwordField = web.FindElement(By.XPath("/html/body/div/div[1]/div/div/table[2]/tbody/tr[2]/td/div/div/form/table/tbody/tr[2]/td/input"));

			loginField.SendKeys(login);
			passwordField.SendKeys(password);

			web.FindElement(By.XPath("/html/body/div/div[1]/div/div/table[2]/tbody/tr[2]/td/div/div/form/table/tbody/tr[4]/td/input[1]"))
				.Click();
		}

	}
}
