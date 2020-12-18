namespace TestApp
{
	public class ClassTest
	{
		public static void Run()
		{
			var type = new Type();
			var result = type.Type;
		}
	}

	public class SomeType
	{
		public string Type { get; set; }

		public SomeType()
		{
			Type = GetType().Name;
		}
	}

	public class Type : SomeType
	{
	}
}
