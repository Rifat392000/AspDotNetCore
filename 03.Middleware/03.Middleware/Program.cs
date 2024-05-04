var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async context =>
{
    await context.Response.WriteAsync("Welcome to All");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Welcome to All again");
});

app.Run();
