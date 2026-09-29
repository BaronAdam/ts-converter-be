using System.Text.Json;
using TSConvert.Models;
using TSConvert.Services;

namespace TSConvert.Tests;

public class ConvertFunctionsTests
{
    private static readonly ResponseService Service = new();

    private static ConvertResponse Read(Microsoft.Azure.Functions.Worker.Http.HttpResponseData response) =>
        JsonSerializer.Deserialize<ConvertResponse>(((FakeHttpResponseData)response).ReadBody())!;

    private static FakeHttpRequestData Req() => FakeHttpRequestData.Create();

    [Theory]
    [InlineData(3, 0, 1)]     // 3 / 3 = 1 min
    [InlineData(180, 1, 0)]   // 60 real minutes
    [InlineData(200, 1, 6)]   // 66.67 -> 1h 6m
    public void AtsCity_DividesByThree(int ingame, int hours, int minutes)
    {
        var body = Read(new ConvertAtsIngameIntoRealTime(Service).City(Req(), ingame));

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }

    [Theory]
    [InlineData(20, 0, 1)]
    [InlineData(1200, 1, 0)]
    public void AtsOutside_DividesByTwenty(int ingame, int hours, int minutes)
    {
        var body = Read(new ConvertAtsIngameIntoRealTime(Service).Outside(Req(), ingame));

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }

    [Theory]
    [InlineData(3, 0, 1)]
    [InlineData(180, 1, 0)]
    public void EtsCity_DividesByThree(int ingame, int hours, int minutes)
    {
        var body = Read(new ConvertEtsIngameIntoRealTime(Service).City(Req(), ingame));

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }

    [Theory]
    [InlineData(19, 0, 1)]
    [InlineData(1140, 1, 0)]
    public void EtsOutsideMainland_DividesByNineteen(int ingame, int hours, int minutes)
    {
        var body = Read(new ConvertEtsIngameIntoRealTime(Service).Outside(Req(), ingame));

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }

    [Theory]
    [InlineData(15, 0, 1)]
    [InlineData(900, 1, 0)]
    public void EtsOutsideUk_DividesByFifteen(int ingame, int hours, int minutes)
    {
        var body = Read(new ConvertEtsIngameIntoRealTime(Service).OutsideUk(Req(), ingame));

        Assert.Equal(hours, body.Hours);
        Assert.Equal(minutes, body.Minutes);
    }
}
