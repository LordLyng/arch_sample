using FluentValidation.AspNetCore;

using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddServiceApplication();
builder.Services.AddCQRSApplication();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddDateOnlyTimeOnlyStringConverters();

// Add services to the container.
builder.Services.AddControllers(options => options.UseNamespaceRouteToken());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(x => x.IsNested ? $"{x.DeclaringType!.Name}{x.Name}" : x.Name);
    c.UseApiEndpoints();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

