using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GameReviews.Application.Common.Logger;
public static class MethodTimeLogger
{
    public static ILogger Logger { get; set; }

    public static void Log(MethodBase methodBase, TimeSpan elapsed, string message)
    {
        Logger.LogInformation("{Class}.{Method} {Duration}",methodBase.DeclaringType!.Name, methodBase.Name, elapsed);
    }
}