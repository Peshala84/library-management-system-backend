using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - SQLite
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=library.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(conn));

// Allow CORS from local dev frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:5173"); // change if your front-end runs on other port
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // applies migrations / creates DB
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocal");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
