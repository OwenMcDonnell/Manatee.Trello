﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		private class ReferenceComparer<T> : IEqualityComparer<T>
		{
			public bool Equals(T x, T y)
			{
				return ReferenceEquals(x, y);
			}
			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}
		}

		[TestMethod]
		public void TestMethod1()
		{
			Run(() =>
				{
					var card = new Card(TrelloIds.CardId);
					Console.WriteLine(card.DueDate);
					card.DueDate = null;
					//var org = new Organization("gamestopit");
					//OutputCollection("boards", org.Boards.Select(b => b.Preferences.Background)
					//							  .Distinct(new ReferenceComparer<BoardBackground>())
					//							  .Select(b => b.Id));
				});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Shutdown();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine("    {0}", item);
			}
		}
	}
}
