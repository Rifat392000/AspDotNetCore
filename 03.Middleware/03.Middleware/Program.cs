using _03.Middleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware1>();

var app = builder.Build();

//Middleware 1

app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("Welcome to All");
   await next(context);
});

//Middleware 2
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Welcome to All again");
   await next(context);
});

app.UseHelloCustomMiddleware();

//CustomMiddleware 1

//app.UseMiddleware<CustomMiddleware1>();
app.UseCustomMiddleware1();


//Middleware 3

app.Run(async context =>
{
    await context.Response.WriteAsync("Howdy");
});

app.Run();
