using FluentAssertions;
using NUnit.Framework;
using PhoneNumbers;

namespace TestApp
{
	[TestFixture]
	public class PhoneNumberTests
	{
		private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

		[TestCase("+779110394606", "RU")]
		[TestCase("+79110394606", "RU")]
		[TestCase("+7911039460", "RU")]
		public void RegionTest(string phoneNumber, string expected)
		{
			var parsedNumber = PhoneNumberUtil.Parse(phoneNumber, null);

			PhoneNumberUtil.GetRegionCodeForCountryCode(parsedNumber.CountryCode).Should().Be(expected);
		}

		[TestCase("+779110394606", false)]
		[TestCase("+79110394606", true)]
		[TestCase("+7911039460", false)]
		[TestCase("+35799726044", true)]
		public void ValidTest(string phoneNumber, bool expected)
		{
			var parsedNumber = PhoneNumberUtil.Parse(phoneNumber, null);

			PhoneNumberUtil.IsValidNumber(parsedNumber).Should().Be(expected);
		}
	}
}
