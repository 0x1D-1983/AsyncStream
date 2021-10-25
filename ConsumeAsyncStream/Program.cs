using System.Net.Http.Json;
using System.Text.Json;
using Ndjson.AsyncStreams.Net.Http;

using HttpClient httpClient = new();

#region Ndjson
using HttpResponseMessage response =
    await httpClient.GetAsync("https://localhost:5001/Heartbeat",
        HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

response.EnsureSuccessStatusCode();
var heartbeats = response.Content!.ReadFromNdjsonAsync<string>().ConfigureAwait(false);

await foreach (string heartbeat in heartbeats)
{
    Console.WriteLine($"{heartbeat}");
}
#endregion

//#region Json --- NOT WORKING
//using HttpResponseMessage response =
//    await httpClient.GetAsync("https://localhost:5001/Heartbeat/stream2",
//        HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

//response.EnsureSuccessStatusCode();
//var heartbeats = await response.Content!
//    .ReadFromJsonAsync<IAsyncEnumerable<string>>().ConfigureAwait(false);

//await foreach (string heartbeat in heartbeats)
//{
//    Console.WriteLine($"{heartbeat}");
//}
//#endregion

