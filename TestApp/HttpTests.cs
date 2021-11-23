using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class HttpTests
	{
		[Test]
		public void MultipartFormData_ShouldContainContentTypeHeader()
		{
			var data = new MultipartFormDataContent();
			data.Add(new StringContent("test"), "test");

			data.Headers.Should().NotBeEmpty();
		}
	}
}
