using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
    [TestFixture]
    public class LinqTests
    {
        [Test]
        public void Test_Union()
        {
            var set1 = new[] { 1, 1, 2, 2 };
            var set2 = new[] { 3, 3, 4, 4 };
            var set = set1.Union(set2);

            set.Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
        }
    }
}
