using System;
using Microsoft.EntityFrameworkCore;

namespace RealEstateWebApp.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RealEstateContext>();
        await dbContext.Database.MigrateAsync();
    }
}
