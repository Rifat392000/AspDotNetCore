var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { WebRootPath ="myroot"});
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to All");
    });
});

app.Run();
