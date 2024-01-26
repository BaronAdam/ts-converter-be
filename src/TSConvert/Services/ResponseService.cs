using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;
using TSConvert.Models;
using TSConvert.Services.Interfaces;

namespace TSConvert.Services;

public class ResponseService : IResponseService
{
    public HttpResponseData CreateResponse(HttpRequestData req, decimal requestMinutes)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");

        var minutes = (int)(requestMinutes % 60);
        var hours = (int)(requestMinutes / 60);

        var responseData = new ConvertResponse
        {
            Minutes = minutes,
            Hours = hours
        };

        var json = JsonSerializer.Serialize(responseData);

        response.WriteString(json);

        return response;
    }
}