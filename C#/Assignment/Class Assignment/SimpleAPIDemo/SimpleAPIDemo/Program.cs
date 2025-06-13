using Microsoft.EntityFrameworkCore;
using SimpleAPIDemo.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the ProductService as a singleton service
builder.Services.AddSingleton<SimpleAPIDemo.Services.IProductService, SimpleAPIDemo.Services.ProductService>();

// Register the CategoryService as a singleton service
builder.Services.AddSingleton<SimpleAPIDemo.Services.ICategoryService, SimpleAPIDemo.Services.CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
