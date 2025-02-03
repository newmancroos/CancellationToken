using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/long-running-request", async (CancellationToken cancellationToken) =>
{
    var randomId = Guid.NewGuid();
    var results = new List<string>();

    for (int i = 0; i < 100; i++)
    {
        if (cancellationToken.IsCancellationRequested)
            return Results.StatusCode(499); //Operation Cancelled
        await Task.Delay(1000);
        var result = $"{randomId} Result {i}";
        Console.WriteLine(result);
        results.Add(result);
    }
    return Results.Ok(results);
})
    .WithName("GetAllData");
    //.WithOpenApi();

app.MapControllers();

app.Run();

//If we call this end-point from Swagger and close the tab the process will be stoped.
