using DotNetPractice.CustomService;
using DotNetPractice.RestApiRedo1.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connection = builder.Configuration.GetConnectionString("DbConnection")!;
builder.Services.AddScoped(n => new AdoDotNetService(connection));
builder.Services.AddScoped(n => new DapperService(connection));
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(connection);
}, ServiceLifetime.Transient, ServiceLifetime.Transient);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
