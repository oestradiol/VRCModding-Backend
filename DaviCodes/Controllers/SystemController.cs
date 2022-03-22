using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace DaviCodes.Controllers; 

[ApiController]
[Route("api/v1/system")]
public class SystemController : ControllerBase {
	public object GetSystemInfo() {
		var version = Assembly.GetEntryAssembly()?.GetName().Version;
		return new {
			version = $"{version?.Major}.{version?.Minor}.{version?.Build}"
		};
	}
}