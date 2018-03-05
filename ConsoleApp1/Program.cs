using System;
using System.Collections.Generic;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.Tests;
using Manatee.Trello.WebApi;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			TestMethod1();
		}

		public static void TestMethod1()
		{
			Run(() =>
				{
					Board board = null;
					try
					{
						var org = new Organization("littlecrabsolutions");
						board = org.Boards.Add("TEST");

						Console.WriteLine(board);
					}
					finally
					{
						board?.Delete();
					}
				});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Flush();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine();
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine($"    {item}");
			}
		}
	}
}
