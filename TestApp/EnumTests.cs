using System;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class EnumTests
	{
		[Test]
		public void Test1()
		{
			default(SomeEnum).ToString().Should().Be("0");
		}

		[Test]
		public void Test2()
		{
			SomeEnum.One.ToString().ToLower().Should().Be("one");
		}

		[Test]
		public void Test3()
		{
			var result1 = Enum.TryParse<SomeEnum>("1", out var enum1);
			var result2 = Enum.TryParse<SomeEnum>("2", out var enum2);
			var resultOne = Enum.TryParse<SomeEnum>("One", out var enumOne);
			var resultTwo = Enum.TryParse<SomeEnum>("Two", out var enumTwo);
			var resultNull = Enum.TryParse<SomeEnum>(null, out var enumNull);

			result1.Should().BeTrue();
			result2.Should().BeTrue();
			resultOne.Should().BeTrue();
			resultTwo.Should().BeTrue();
			resultNull.Should().BeFalse();

			enum1.Should().Be(SomeEnum.One);
			enum2.Should().Be(SomeEnum.Two);
			enumOne.Should().Be(SomeEnum.One);
			enumTwo.Should().Be(SomeEnum.Two);
			enumNull.Should().Be(default(SomeEnum));
		}
	}

	public enum SomeEnum
	{
		One = 1, Two = 2
	}
}
