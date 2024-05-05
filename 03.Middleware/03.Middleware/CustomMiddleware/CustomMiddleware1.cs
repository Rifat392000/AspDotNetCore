namespace _03.Middleware.CustomMiddleware
{
    public class CustomMiddleware1 : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
          await  context.Response.WriteAsync("Custom Middleware 1 start");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware 1 End");
        }
    }


  public  static class CustomExtention
    {
        public static IApplicationBuilder UseCustomMiddleware1(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddleware1>();
        }
    }
}
