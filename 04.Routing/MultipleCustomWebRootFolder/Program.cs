using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { WebRootPath = "myroot" });
var app = builder.Build();

app.UseStaticFiles();

//if the same file existing in both first one execute because it checked it first

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
});

//builder.Environment.ContentRootPath

// J:\AspDotNetCore\04.Routing\MultipleCustomWebRootFolder


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "customFile"))
});

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to All");
    });
});

app.Run();