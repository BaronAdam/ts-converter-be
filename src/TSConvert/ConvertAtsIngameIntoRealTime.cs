using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using TSConvert.Models;

namespace TSConvert;

public class ConvertAtsIngameIntoRealTime
{
    [Function("ConvertAtsIngameCityIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameCityIntoRealTime")]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes inside city to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData City([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ats/city/{minutes:int}")] HttpRequestData req)
    {
        var requestMinutes = Convert.ToInt32(req.Url.AbsoluteUri.Split('/').Last()) / 3m;

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");

        var minutes = (int)(requestMinutes % 60);
        var hours = (int)(requestMinutes / 60);

        var responseData = new ConvertResponse {
            Minutes = minutes,
            Hours = hours
        };

        var json = JsonSerializer.Serialize(responseData);

        response.WriteString(json);

        return response;
    }

    [Function("ConvertAtsIngameOutsideIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameOutsideIntoRealTime")]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes outside of city to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData Outside([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ats/outside/{minutes:int}")] HttpRequestData req)
    {
        var requestMinutes = Convert.ToInt32(req.Url.AbsoluteUri.Split('/').Last()) / 20m;

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");

        var minutes = (int)(requestMinutes % 60);
        var hours = (int)(requestMinutes / 60);

        var responseData = new ConvertResponse {
            Minutes = minutes,
            Hours = hours
        };

        var json = JsonSerializer.Serialize(responseData);

        response.WriteString(json);

        return response;
    }
}
