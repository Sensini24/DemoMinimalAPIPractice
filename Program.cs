using DemoMinimalAPI.Data;
using Microsoft.EntityFrameworkCore;
using DemoMinimalAPI.Entities;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/getnameUser", (string nombre) => $"Hola muchacho guapo llamado {nombre}");
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/suma", (int a, int b) => Results.Ok(a + b));

app.MapGet("/getBooks", async (DataContext db) =>
{
    var _db = db;
    var books = _db.Books.ToList();
    return Results.Ok(books);
});

app.MapPost("/saveBook", async (Book book, DataContext db) =>
{
    var _db = db;
    _db.Books.Add(book);
    await _db.SaveChangesAsync();

    return Results.Ok("Libro guardado correctamente");
})

.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
