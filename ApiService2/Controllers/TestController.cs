using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiService2.Controllers
{
	[ApiController]
	[Route("test")]
	public class TestController : ControllerBase
	{
		private readonly ILogger<TestController> _logger;
		private readonly HttpClient _httpClient;

		public TestController(ILogger<TestController> logger)
		{
			_logger = logger;
			_httpClient = new HttpClient();
		}

		[HttpGet]
		[Route("test1")]
		public IActionResult Test1()
		{
			return new ObjectResult("Hello from second service!");
		}

		[HttpGet]
		[Route("test2")]
		public async Task<IActionResult> Test2(string path)
		{
			try
			{
				var result = await _httpClient.GetStringAsync(path);
				return new ObjectResult(result);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "error");
				return null;
			}
		}
	}
}
