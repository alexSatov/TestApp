using System;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class JsonTest
	{
		[Test]
		public void Test1()
		{
			var aType = new AType { Base = "BaseA", A = "A", Date = DateTime.Now };
			var container = new Container { Type = aType.GetType().Name, Object = aType };
			var json = JsonConvert.SerializeObject(container);
			var deserialized = JsonConvert.DeserializeObject<Container>(json);
			var a = (JObject) deserialized.Object;
			var aTypeDeserialized = a.ToObject<AType>();
		}

		[Test]
		public void Test2()
		{
			var jsonArr = "[\"1\", \"2\"]";
			var arr = JsonConvert.DeserializeObject<string[]>(jsonArr);
			arr.Should().BeEquivalentTo(new []{ "1", "2" });
		}

		private abstract class BaseType
		{
			public string Base { get; set; }
		}

		private class AType : BaseType
		{
			public string A { get; set; }
			public DateTime Date { get; set; }
		}

		private class Container
		{
			public string Type { get; set; }
			public object Object { get; set; }
		}
	}
}
