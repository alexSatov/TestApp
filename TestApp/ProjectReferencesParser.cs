using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TestApp
{
	public class ProjectReferencesParser
	{
		private static readonly Regex nugetPackageRegex = new Regex(
			@"<package id=""(?<Id>.*?)"" version=""(?<Version>.*?)"".*?/>");

		private static readonly Regex projectReferenceRegex = new Regex(
			@"<ProjectReference Include=""(?<Include>.*?)"">.*?</ProjectReference>", RegexOptions.Singleline);

		static void Run(string[] args)
		{
			CsprojToProjectReferences("C:\\src\\tinkofftelephony\\TCSBank.WebMockService\\TCSBank.WebMockService.csproj");
		}

		private static void PackagesConfigToPackageReferences(string path)
		{
			var config = File.ReadAllText(path);

			foreach (Match match in nugetPackageRegex.Matches(config))
			{
				Console.WriteLine($"<PackageReference Include=\"{match.Groups["Id"]}\" Version=\"{match.Groups["Version"]}\" />");
			}
		}

		private static void CsprojToProjectReferences(string path)
		{
			var config = File.ReadAllText(path);

			foreach (Match match in projectReferenceRegex.Matches(config))
			{
				Console.WriteLine($"<ProjectReference Include=\"{match.Groups["Include"]}\" />");
			}
		}
	}
}
