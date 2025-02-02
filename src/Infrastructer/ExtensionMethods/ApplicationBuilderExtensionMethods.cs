﻿using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

 

namespace Infrastructure.ExtensionMethods
{
    public static class ApplicationBuilderExtensionMethods
    {
        public static async Task EnsureDatabaseCreated(this IApplicationBuilder app, CancellationToken cancellationToken = default)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EventApiDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }

}
