using System.Text.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var connectionString = builder.Configuration.GetConnectionString("ProjectBLocalConnection");


// Add services to the container.
builder.Services.AddControllers();;
builder.Services.AddSingleton<IRequestValidator, RequestValidator>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton<IUsersDataservice, UsersDataservice>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "manage-users", 
            Version = "v1", 
            Description = "An ASP.NET Core Web API for managing Users",
            Contact = new OpenApiContact
                    {
                        Name = "Tyler Simeone",
                        Url = new Uri("https://github.com/tyler-simeone")
                    },
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "manage-users v1");
    });
    // app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();