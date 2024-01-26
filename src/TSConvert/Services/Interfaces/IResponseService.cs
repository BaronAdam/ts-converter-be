using Microsoft.Azure.Functions.Worker.Http;

namespace TSConvert.Services.Interfaces;

public interface IResponseService
{
    HttpResponseData CreateResponse(HttpRequestData req, decimal requestMinutes);
}