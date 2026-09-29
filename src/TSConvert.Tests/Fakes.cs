using System.Net;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using NSubstitute;

namespace TSConvert.Tests;

internal sealed class FakeHttpRequestData(FunctionContext context, Uri url) : HttpRequestData(context)
{
    public override Stream Body { get; } = new MemoryStream();
    public override HttpHeadersCollection Headers { get; } = new();
    public override IReadOnlyCollection<IHttpCookie> Cookies { get; } = [];
    public override Uri Url { get; } = url;
    public override IEnumerable<ClaimsIdentity> Identities { get; } = [];
    public override string Method => "GET";

    public override HttpResponseData CreateResponse() => new FakeHttpResponseData(FunctionContext);

    public static FakeHttpRequestData Create(string url = "http://localhost/api/test") =>
        new(Substitute.For<FunctionContext>(), new Uri(url));
}

internal sealed class FakeHttpResponseData(FunctionContext context) : HttpResponseData(context)
{
    public override HttpStatusCode StatusCode { get; set; }
    public override HttpHeadersCollection Headers { get; set; } = new();
    public override Stream Body { get; set; } = new MemoryStream();
    public override HttpCookies Cookies => throw new NotSupportedException();

    public string ReadBody()
    {
        Body.Position = 0;
        return new StreamReader(Body).ReadToEnd();
    }
}
