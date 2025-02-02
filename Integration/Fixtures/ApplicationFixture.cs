﻿

namespace Integration.Fixtures
{
    public class ApplicationFixture : IDisposable
    {
        internal IServiceProvider Services { get; }

        protected IConfiguration Configuration { get; }

        protected IWebHostEnvironment Environment { get; }

        protected WebApplication Application { get; }

        internal EventApiDbContext DataContext { get; }
         
        public ApplicationFixture()
        {
            System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");
            var builder = WebApplication.CreateBuilder(Array.Empty<string>());
            builder.Configuration.AddAppSettings();
            builder.Services.AddInfrastructure(builder.Configuration);
            Application = builder.Build();

            var serviceScope = Application.Services.CreateScope();
            Services = serviceScope.ServiceProvider;
            Configuration = Application.Configuration;
            Environment = Application.Environment;

            DataContext = Services.GetRequiredService<EventApiDbContext>();

            DataContext.Database.EnsureDeleted();
            DataContext.Database.Migrate();
            DataContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) 
            {
                DataContext.Database.EnsureDeleted();
                DataContext.Dispose();
            }
        }
        public async Task ClearDatabaseAsync()
        {
            var entityTypes = DataContext.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Distinct()
                .ToList();

            foreach (var table in entityTypes)
            {
                await DataContext.Database.ExecuteSqlRawAsync($"DELETE FROM [{table}]");
            }
            await DataContext.SaveChangesAsync();
        }


    }
}
