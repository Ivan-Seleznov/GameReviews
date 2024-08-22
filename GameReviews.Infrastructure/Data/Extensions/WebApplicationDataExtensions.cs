using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameReviews.Infrastructure.Data.Extensions;
public static class WebApplicationDataExtensions
{
    public static async Task<IHost> ApplyMigrations(this IHost app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        return app;
    }
}