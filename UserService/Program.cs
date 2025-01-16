using Microsoft.EntityFrameworkCore;
using UsersService.Data;
using UsersService.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString  = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"---> connection string: {connectionString}");

builder.Services.AddDbContext<AppDbContext>(opt =>

opt.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddServices(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
////{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
Console.WriteLine("now swagger in prodcution");
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
