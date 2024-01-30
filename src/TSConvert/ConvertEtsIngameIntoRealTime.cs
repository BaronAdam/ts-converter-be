using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using TSConvert.Models;
using TSConvert.Services.Interfaces;

namespace TSConvert;

public class ConvertEtsIngameIntoRealTime(IResponseService responseService)
{
    [Function("ConvertEtsIngameCityIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameCityIntoRealTime", tags: ["ETS"])]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes inside city to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData City(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ets/city/{minutes:int}")] HttpRequestData req,
        [FromRoute] int minutes)
    {
        var requestMinutes = minutes / 3m;

        return responseService.CreateResponse(req, requestMinutes);
    }

    [Function("ConvertEtsIngameOutsideMainlandIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameOutsideMainlandIntoRealTime", tags: ["ETS"])]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes outside of city on the mainland Europe to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData Outside(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ets/outside/mainland/{minutes:int}")] HttpRequestData req,
        [FromRoute] int minutes)
    {
        var requestMinutes = minutes / 19m;

        return responseService.CreateResponse(req, requestMinutes);
    }

    [Function("ConvertEtsIngameOutsideUkIntoRealTime")]
    [OpenApiOperation(operationId: "ConvertAtsIngameOutsideUkIntoRealTime", tags: ["ETS"])]
    [OpenApiParameter(name: "minutes", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "Number of minutes outside of city in the UK to convert to real time")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ConvertResponse), Description = "Ingame time converted to real time")]
    public HttpResponseData OutsideUk(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "convert/ets/outside/uk/{minutes:int}")] HttpRequestData req,
        [FromRoute] int minutes)
    {
        var requestMinutes = minutes / 15m;

        return responseService.CreateResponse(req, requestMinutes);
    }
}
