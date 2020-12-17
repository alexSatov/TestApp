using System;
using System.Collections;
using System.Collections.Generic;

namespace TestApp
{
	public class TypeMatchingTest
	{
		public static void Run()
		{
			var obj = new List<int>();
			var type = obj.GetType();
			var interfaces = type.GetInterfaces();
		}
	}
}
