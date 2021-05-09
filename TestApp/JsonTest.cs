using System;
using System.Text.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestApp
{
	[TestFixture]
	public class JsonTest // Newtonsoft.Json win System.Text.Json
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
		public void Test1_2()
		{
			var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
			var aType = new AType { Base = "BaseA", A = "A", Date = DateTime.Now };
			var baseType = (BaseType) aType;
			var json = JsonConvert.SerializeObject(baseType, settings);
			var deserialized = JsonConvert.DeserializeObject<BaseType>(json, settings);
		}

		[Test]
		public void Test2()
		{
			var jsonArr = "[\"1\", \"2\"]";
			var arr = JsonConvert.DeserializeObject<string[]>(jsonArr);
			arr.Should().BeEquivalentTo(new []{ "1", "2" });
		}

		[Test]
		public void Test3()
		{
			var obj = new ClassWithEnum { Value = "test", EnumValue = SomeEnum.Two };
			var json = JsonConvert.SerializeObject(obj);
			var obj2 = JsonConvert.DeserializeObject<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": 2}");
			var obj3 = JsonConvert.DeserializeObject<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": \"Two\"}");
			var obj4 = JsonConvert.DeserializeObject<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": \"two\"}");

			obj.Should().BeEquivalentTo(obj2);
			obj.Should().BeEquivalentTo(obj3);
			obj.Should().BeEquivalentTo(obj4);
		}

		[Test]
		public void Test1_SystemJson()
		{
			var aType = new AType { Base = "BaseA", A = "A", Date = DateTime.Now };
			var container = new Container { Type = aType.GetType().Name, Object = aType };
			var json = JsonSerializer.Serialize(container);
			var deserialized = JsonSerializer.Deserialize<Container>(json);
			var a = (JsonElement) deserialized.Object;
			var aTypeDeserialized = JsonSerializer.Deserialize<AType>(a.GetRawText());
		}

		[Test]
		public void Test1_2_SystemJson()
		{
			var aType = new AType { Base = "BaseA", A = "A", Date = DateTime.Now };
			var baseType = (BaseType) aType;
			var json = JsonSerializer.Serialize(baseType);
			var deserialized = JsonSerializer.Deserialize<AType>(json);
		}

		[Test]
		public void Test2_SystemJson()
		{
			var jsonArr = "[\"1\", \"2\"]";
			var arr = JsonConvert.DeserializeObject<string[]>(jsonArr);
			arr.Should().BeEquivalentTo("1", "2");
		}

		[Test]
		public void Test3_SystemJson() // fails
		{
			var obj = new ClassWithEnum { Value = "test", EnumValue = SomeEnum.Two };
			var json = JsonSerializer.Serialize(obj);
			var obj2 = JsonSerializer.Deserialize<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": 2}");
			var obj3 = JsonSerializer.Deserialize<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": \"Two\"}");
			var obj4 = JsonSerializer.Deserialize<ClassWithEnum>("{\"Value\": \"test\", \"EnumValue\": \"two\"}");

			obj.Should().BeEquivalentTo(obj2);
			obj.Should().BeEquivalentTo(obj3);
			obj.Should().BeEquivalentTo(obj4);
		}

		[Test]
		public void Test_Record()
		{
			var record = new TestRecord("test", 1);
			var json = JsonConvert.SerializeObject(record);
			var jsonRecord = JsonConvert.DeserializeObject<TestRecord>(json);

			record.Should().BeEquivalentTo(jsonRecord);
		}

		[Test]
		public void Test_InitClass()
		{
			var obj = new TestInitClass { StringValue = "test", IntValue = 1 };
			var json = JsonConvert.SerializeObject(obj);
			var jsonObj = JsonConvert.DeserializeObject<TestInitClass>(json);

			obj.Should().BeEquivalentTo(jsonObj);
		}

		[Test]
		public void Test_InitClass2()
		{
			var obj = new TestInitClass("test", 1);
			var json = JsonConvert.SerializeObject(obj);
			var jsonObj = JsonConvert.DeserializeObject<TestInitClass>(json);

			obj.Should().BeEquivalentTo(jsonObj);
		}

		[Test]
		public void Test_ReadonlyStruct()
		{
			var obj = new TestStruct { StringValue = "test", IntValue = 1 };
			var json = JsonConvert.SerializeObject(obj);
			var jsonObj = JsonConvert.DeserializeObject<TestStruct>(json);

			obj.Should().BeEquivalentTo(jsonObj);
		}

		private record TestRecord(string StringValue, int IntValue);

		private class TestInitClass
		{
			public string StringValue { get; init; }
			public int IntValue { get; init; }

			public TestInitClass(string stringValue, int intValue)
			{
				StringValue = stringValue;
				IntValue = intValue;
			}

			public TestInitClass()
			{
			}
		}

		private readonly struct TestStruct
		{
			public string StringValue { get; init; }
			public int IntValue { get; init; }

			public TestStruct(string stringValue, int intValue)
			{
				StringValue = stringValue;
				IntValue = intValue;
			}
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

		private class ClassWithEnum
		{
			public string Value { get; set; }
			public SomeEnum EnumValue { get; set; }
		}

		private enum SomeEnum
		{
			One = 1,
			Two = 2
		}
	}
}
