namespace Werter.HelloQuery.Api.Configurations;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.UseCors("Total");

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}