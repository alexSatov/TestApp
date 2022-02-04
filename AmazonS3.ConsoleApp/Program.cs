using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace AmazonS3.ConsoleApp
{
	internal class Program
	{
		internal static async Task Main(string[] args)
		{
			var client = new AmazonS3Client(null, null, new AmazonS3Config
			{
			});

			var files = await client.ListObjectsV2Async(new ListObjectsV2Request
			{
			}).ConfigureAwait(false);

			var actualFile = files.S3Objects.OrderByDescending(f => f.LastModified).First();
			var response = await client.GetObjectAsync(actualFile.BucketName, actualFile.Key).ConfigureAwait(false);

			using var streamReader = new StreamReader(response.ResponseStream);

			while (!streamReader.EndOfStream)
			{
				var line = await streamReader.ReadLineAsync();
				Console.WriteLine(line);
			}
		}
	}
}
