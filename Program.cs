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

// Add the OperationNotAllowedExceptionFilter to scope
builder.Services.AddControllers(options =>
{
  options.Filters.Add<OperationNotAllowedExceptionFilter>();
});

// Add services to scope
builder.Services.AddScoped<UserInfoService>(); // Add the UserInfoService to scope
builder.Services.AddScoped<PostService>(); // Add the PostService to scope
builder.Services.AddScoped<ReviewService>(); // Add the ReviewService to scope

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger(); // Enable Swagger/OpenAPI
  app.UseSwaggerUI(); // Enable Swagger/OpenAPI UI
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

app.UseAuthorization(); // Enable authorization

app.MapControllers(); // Map the controllers

app.Run(); // Run the app
