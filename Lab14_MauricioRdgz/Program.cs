using Lab14_MauricioRdgz.Application;
using Lab14_MauricioRdgz.Infrastructure;
using Lab14_MauricioRdgz.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ErroresHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { estado = "OK", aplicacion = "Lab14_MauricioRdgz" }));
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
