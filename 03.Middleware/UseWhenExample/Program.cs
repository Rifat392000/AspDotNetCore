var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(

    context => context.Request.Query.ContainsKey("UserId"),

    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello Use When");
            await next();
        });
    }
    
    
    );

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello Main Chain");

});

app.Run();
