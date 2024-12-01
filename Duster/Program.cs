using Duster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExecutionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("db"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEnterPathService, EnterPathService>();
builder.Services.AddScoped<IExecutionRepository, ExecutionRepository>();

var app = builder.Build();

// apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ExecutionDbContext>();
    dbContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();


