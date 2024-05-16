using RoutingExamples.CustomConstraints;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRouting(options => {
    options.ConstraintMap.Add("months", typeof( CustomConstraintsExp));
});


var app = builder.Build();

app.Use(async (context, next) =>
{
    
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    if (endPoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endPoint.DisplayName}\n");
    }
    await next(context);
});



//enable routing
app.UseRouting();

app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    if (endPoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endPoint.DisplayName}\n");
    }
    await next(context);
});
//creating routing
app.UseEndpoints(endpoints =>
{
    endpoints.Map("userAuth", async contest =>
    {
        await contest.Response.WriteAsync("General Routhing use each case get, post, etc");
    });

    endpoints.MapGet("user1", async contest =>
    {
        await contest.Response.WriteAsync("Get Access Only");
    });

    endpoints.MapPost("user2", async contest =>
    {
        await contest.Response.WriteAsync("Post Access");
    });


    //route parameters are case insensative

    endpoints.Map("files/{filename}.{extension}", async context =>
    {
       string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        await context.Response.WriteAsync($"Filename is {filename} and file extention is {Convert.ToString(context.Request.RouteValues["Extension"])} ");
    });


    endpoints.Map("Office/worker/{empname}", async context =>
    {
        string? empname = Convert.ToString(context.Request.RouteValues["empname"]);
        await context.Response.WriteAsync($"{empname}");
    });

    //default parameters set

    endpoints.Map("Videos/movies/{types=Action}", async c =>
    {
        string? type = Convert.ToString(c.Request.RouteValues["types"]);
        await c.Response.WriteAsync($"The movie jaenra {type}");
    });

    endpoints.Map("products/Details/{id=5}", async context =>
    {
        int value = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"Product id is {value}");
    });

    endpoints.Map("products/category/{id?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int v = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product category id is {v}");
        }

        else
        {
            await context.Response.WriteAsync("Product category id is not provided");
        }
    });


    endpoints.Map("products/{id:int?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int v = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product id is {v}");
        }

        else
        {
            await context.Response.WriteAsync("Product id is not provided");
        }
    });

    endpoints.Map("timer-is-on/{get-time:datetime}", async context =>
    {
        DateTime dt = Convert.ToDateTime(context.Request.RouteValues["get-time"]);
        await context.Response.WriteAsync($"{dt}");
    });


    //GUID
    endpoints.Map("cities/{citiesId}", async context =>
    {
        Guid id = Guid.Parse(Convert.ToString(context.Request.RouteValues["citiesID"])!);
        await context.Response.WriteAsync($"Guid (globally unique identifier) is {id}");
    });


    endpoints.Map("office/emp-min/{ename:minlength(4)}", async context =>
    {
        string? v = Convert.ToString(context.Request.RouteValues["ename"]);
        await context.Response.WriteAsync(v!);
    });

    endpoints.Map("office/emp-max/{ename:maxlength(14)}", async context =>
    {
        string? v = Convert.ToString(context.Request.RouteValues["ename"]);
        await context.Response.WriteAsync(v!);
    });

    endpoints.Map("office/emp-min-max/{ename:minlength(4):maxlength(10)}", async context =>
    {
        string? v = Convert.ToString(context.Request.RouteValues["ename"]);
        await context.Response.WriteAsync(v!);
    });

    endpoints.Map("office/length/{ename:length(4,10):alpha}", async context =>
    {
        string? v = Convert.ToString(context.Request.RouteValues["ename"]);
        await context.Response.WriteAsync(v!);
    });

    //sales-report/2030/apr
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context =>
    {

        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"sales report - {year} - {month}");
        }
        else
        {
            await context.Response.WriteAsync($"{month} is not allowed for sales report");
        }
    });




    //sales/2030/apr
    endpoints.Map("sales/{year:int:min(1900)}/{month:months}", async context =>
    {

        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"sales report - {year} - {month}");
        }
        else
        {
            await context.Response.WriteAsync($"{month} is not allowed for sales report");
        }
    });


    endpoints.Map("products/details-num/{id:int:range(1,1000)?}", async context => {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Products details - {id}");
        }
        else
        {
            await context.Response.WriteAsync($"Products details - id is not supplied");
        }
    });


    //higher precedences literals

    //sales/2024/jan
    endpoints.Map("sales/2024/jan", async context =>
    {
        await context.Response.WriteAsync("Sales report exclusively for 2024 - jan");
    });

});




app.Run(async con =>
{
    await con.Response.WriteAsync($"{con.Request.Path}");
});

app.Run();
