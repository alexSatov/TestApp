using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class GotoTests
	{
		[TestCase(true)]
		[TestCase(false)]
		public void GotoDefault(bool flag)
		{
			const int value = 1;
			int result;

			switch (value)
			{
				case 1:
					if (flag)
					{
						goto default;
					}
					result = 2;
					break;
				default:
					result = 100;
					break;
			}

			result.Should().Be(flag ? 100 : 2);
		}
	}
}
