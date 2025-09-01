using Tech_task_back_end.Infrastructure;
using Tech_task_back_end.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure();
builder.Services.AddRepositories();
builder.Services.AddMapper();
builder.Services.AddRequestHandlers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DatabaseInitializer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

using (IServiceScope? scope = app.Services.CreateScope())
{
    DatabaseInitializer? initializer = scope.ServiceProvider.GetService<DatabaseInitializer>();
    initializer?.Initialize();
}

app.Run();