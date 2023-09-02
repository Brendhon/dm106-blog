using Blog.Data;
using Blog.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
builder.Services.AddLogging(loggingBuilder =>
 {
   loggingBuilder.AddConsole();
   loggingBuilder.AddDebug();
   loggingBuilder.AddAzureWebAppDiagnostics();
 });

// Add the BlogContext to db context
builder.Services.AddDbContext<BlogContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:AZURE_SQL_CONNECTION"]));

// Add the UserInfoService to scope
builder.Services.AddScoped<UserInfoService>();

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
