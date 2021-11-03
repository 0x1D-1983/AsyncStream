using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ndjson.AsyncStreams.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AsyncStreamApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HeartbeatController : Controller
{
    [HttpGet]
    public NdjsonAsyncEnumerableResult<string> Get(CancellationToken cancel)
    {
        return new NdjsonAsyncEnumerableResult<string>(GetStream(cancel));
    }

    [HttpGet("stream2")]
    public IAsyncEnumerable<string> Get2(CancellationToken cancel)
    {
        return GetStream(cancel);
    }

    private async IAsyncEnumerable<string> GetStream(
        [EnumeratorCancellation]CancellationToken cancel)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync(cancel))
        {
            yield return $"{DateTime.Now.ToLongTimeString()}";
        }
    }
}
