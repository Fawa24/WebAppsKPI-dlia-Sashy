using Lab_1.Interfaces;
using Lab_1.Services;
using Lab_2.Databases;
using Lab_2.Interfaces;
using Lab_2.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
