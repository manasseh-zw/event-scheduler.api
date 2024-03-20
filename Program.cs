using event_scheduler.api.Data.Models;
using event_scheduler.api.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurePostgresContext(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.Scan(x =>
    x.FromAssemblies(typeof(Program).Assembly)
    .AddClasses()
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime()
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
        });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
