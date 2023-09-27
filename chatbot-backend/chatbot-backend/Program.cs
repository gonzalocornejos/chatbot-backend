using chatbot_backend.Core.Services.Interfaces;
using chatbot_backend.Core.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using chatbot_backend.Core.Integrations.Interfaces;
using chatbot_backend.Core.Integrations;
using chatbot_backend.Core.Middleware;
using chatbot_backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIConfiguration>(builder.Configuration.GetSection("OpenAI"));
builder.Services.AddControllers();

var services = builder.Services;

services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); ;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatbot API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

services.AddCors(options =>
{
    options.AddPolicy("Cors", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Singletons for Injection Dependencies
// Services
services.AddScoped<IChatbotService, ChatbotService>();

// Repositories

// Integrations
services.AddScoped<IOpenAIIntegration, OpenAIIntegration>();

// AutoMapper

var app = builder.Build();
var environment = app.Environment;
// Configure the HTTP request pipeline.
if (environment.IsDevelopment() || environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chatbot API V1"); });
}
else if (environment.IsProduction())
{
    app.UseHttpsRedirection();
}
else
{
    throw new Exception("Invalid environment");
}

app.UseMiddleware<ExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Cors");

app.Run();