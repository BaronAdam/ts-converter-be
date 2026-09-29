using System.Net;
using System.Text.Json;
using TSConvert.Models;
using TSConvert.Services;

namespace TSConvert.Tests;

public class ResponseServiceTests
{
    private static (HttpResponseDataResult Response, ConvertResponse Body) Run(decimal requestMinutes)
    {
        var response = (FakeHttpResponseData)new ResponseService()
            .CreateResponse(FakeHttpRequestData.Create(), requestMinutes);
        var body = JsonSerializer.Deserialize<ConvertResponse>(response.ReadBody())!;
        return (new HttpResponseDataResult(response), body);
    }

    private sealed record HttpResponseDataResult(FakeHttpResponseData Response);

    [Fact]
    public void ReturnsOkWithJsonContentType()
    {
        var (result, _) = Run(10m);

        Assert.Equal(HttpStatusCode.OK, result.Response.StatusCode);
        Assert.Contains(result.Response.Headers, h =>
            h.Key == "Content-Type" && h.Value.Contains("application/json"));
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(59, 0, 59)]
    [InlineData(60, 1, 0)]
    [InlineData(61, 1, 1)]
    [InlineData(150, 2, 30)]
    [InlineData(1440, 24, 0)]
    public void SplitsWholeMinutesIntoHoursAndMinutes(decimal input, int hours, int minutes)
    {
        var (_, body) = Run(input);

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }

    [Fact]
    public void TruncatesFractionalMinutes()
    {
        // 100 / 3 = 33.33.. minutes
        var (_, body) = Run(100m / 3m);

        Assert.Equal(0, body.Hours);
        Assert.Equal(33, body.Minutes);
    }
}
