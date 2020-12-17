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

	public class BaseType
	{
		public string Type { get; set; }

		public BaseType()
		{
			Type = GetType().Name;
		}
	}

	public class Type : BaseType
	{
	}
}
