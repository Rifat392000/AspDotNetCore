using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run( async (HttpContext context) =>
{

    if (1 == 1)
    {
        context.Response.StatusCode = 200;

    }
    else
    {
        context.Response.StatusCode = 400;

    }

    context.Response.Headers["Content-Type"] = "text/html";


    context.Response.Headers["MyKey"] = "My value";
    context.Response.Headers["Server"] ="My Kestrel server";
    

    await context.Response.WriteAsync("<h2>Welcome to all</h2>");
    await context.Response.WriteAsync("<h3>Hello World</h3>");


    string path = context.Request.Path;
    string method = context.Request.Method;
    await context.Response.WriteAsync($"<p>{path} <br/> {method}</p>");



    if(context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {

            string id = context.Request.Query["id"];

            await context.Response.WriteAsync($"<p>Here user id is {id}</p>");
        }
    }

   
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        string userAgent = context.Request.Headers["User-Agent"];
        await context.Response.WriteAsync($"<p>Here user agent is {userAgent}</p>");
    }

    if(context.Request.Headers.ContainsKey("AuthorizationKey"))
    {
        string auth = context.Request.Headers["AuthorizationKey"];
        await context.Response.WriteAsync($"<p>Here user agent is {auth}</p>");
    }


    System.IO.StreamReader reader = new StreamReader(context.Request.Body);
    string content = await reader.ReadToEndAsync();

                                             //Microsoft.AspNetCore.WebUtilities
    //Microsoft.Extensions.Primitives
    Dictionary<string, StringValues> dt =  QueryHelpers.ParseQuery(content);

    if (dt.ContainsKey("fname"))
    {
        string fn = dt["fname"][0];
        await context.Response.WriteAsync($"<p>Post Request Name is {fn}</p>");
    }
    if (dt.ContainsKey("age"))
    {
        foreach(string age in dt["age"])
        {
            await context.Response.WriteAsync($"<p>{age}</p>");
        }
    }


});



app.Run();

/*Explicit data types make your code clearer by stating the expected values for parameters. This becomes especially helpful when dealing with multiple lambda expressions or when revisiting code after some time.*/


// Without data types (less clear)
// (x) => x * 2;  // What kind of value is x?

// With data types (more readable)
// (int x) => x * 2;  // Clearly expects an integer


// Without data types (potential runtime error)
//string name = "Alice";
//(x) => x.Length;  // This would throw an error because 'Length' is not applicable to strings

// With data types (type checking prevents error)
//string name = "Alice";
//(string x) => x.Length;  // Valid for strings

