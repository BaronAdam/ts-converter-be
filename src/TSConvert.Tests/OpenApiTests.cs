using System.Reflection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace TSConvert.Tests;

public class OpenApiTests
{
    private static readonly MethodInfo[] Functions = typeof(ConvertAtsIngameIntoRealTime).Assembly
        .GetTypes()
        .SelectMany(t => t.GetMethods())
        .Where(m => m.GetCustomAttribute<FunctionAttribute>() is not null)
        .ToArray();

    [Fact]
    public void OperationIdsAreUnique()
    {
        var ids = Functions.Select(m => m.GetCustomAttribute<OpenApiOperationAttribute>()!.OperationId).ToList();

        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Fact]
    public void FunctionNamesAreUnique()
    {
        var names = Functions.Select(m => m.GetCustomAttribute<FunctionAttribute>()!.Name).ToList();

        Assert.Equal(names.Count, names.Distinct().Count());
    }

    [Fact]
    public void RoutesAreUnique()
    {
        var routes = Functions
            .Select(m => m.GetParameters().Select(p => p.GetCustomAttribute<HttpTriggerAttribute>()).First(a => a is not null)!.Route)
            .ToList();

        Assert.Equal(routes.Count, routes.Distinct().Count());
    }
}
