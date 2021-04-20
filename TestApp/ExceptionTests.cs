using System;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class ExceptionTests
	{
		[Test]
		public void DataEmpty()
		{
			var exception = new ArgumentException("Message");
			exception.Data.Should().BeEmpty();
		}
	}
}
