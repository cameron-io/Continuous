var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/forecast", () =>
{
    var summaries = new Dictionary<string, int>{
        {"Freezing", 0},
        {"Bracing", 5},
        {"Chilly", 10}
    };
    var forecast = summaries.Keys.Select(index =>
        new Forecast
        (
            DateOnly.FromDateTime(DateTime.Now),
            summaries[index],
            index
        ))
        .ToArray();
    return forecast;
})
.WithName("GetForecast")
.WithOpenApi();

app.Run();

record Forecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
