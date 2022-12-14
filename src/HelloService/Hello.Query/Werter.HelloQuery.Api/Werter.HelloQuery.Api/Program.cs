using Werter.HelloQuery.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(builder.Host);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseApiConfiguration(app.Environment);

app.Run();