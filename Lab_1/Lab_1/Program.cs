using Lab_1.Interfaces;
using Lab_1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ICustomersService, CustomersService>();
builder.Services.AddSingleton<IOrdersService, OrdersService>();
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
