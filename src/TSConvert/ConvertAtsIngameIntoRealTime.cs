using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using TSConvert.Models;
using TSConvert.Services.Interfaces;

namespace TSConvert;

public class ConvertAtsIngameIntoRealTime(IResponseService responseService)
{
    [Function("ConvertAtsIngameCityIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameCityIntoRealTime", tags: ["ATS"])]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes inside city to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData City([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ats/city/{minutes:int}")] HttpRequestData req)
    {
        var requestMinutes = Convert.ToInt32(req.Url.AbsoluteUri.Split('/').Last()) / 3m;

        return responseService.CreateResponse(req, requestMinutes);
    }

    [Function("ConvertAtsIngameOutsideIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameOutsideIntoRealTime", tags: ["ATS"])]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes outside of city to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData Outside([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ats/outside/{minutes:int}")] HttpRequestData req)
    {
        var requestMinutes = Convert.ToInt32(req.Url.AbsoluteUri.Split('/').Last()) / 20m;

        return responseService.CreateResponse(req, requestMinutes);
    }
}
