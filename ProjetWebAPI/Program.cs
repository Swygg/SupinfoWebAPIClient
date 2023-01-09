using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using ProjetWebAPI.Models.Outputs;
using ProjetWebAPI.Services.Interfaces;
using ProjetWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//String connection injection
builder.Services.AddDbContext<DbFactoryContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Production"));
});

//Automapper injection
var mapperConfig = new MapperConfiguration(
    mc =>
    {
        //Input
        mc.CreateMap<CustomerCreateInput, Customer>();
        mc.CreateMap<CustomerUpdateInput, Customer>();

        //Output
        mc.CreateMap<Customer, CustomerOutput>();
    });
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//Services injection
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Middleware A
app.Use(async (context, next) =>
{
    Console.WriteLine("A (before)");
    await next();               // va appeler le middleware suivant
    Console.WriteLine("A (after)");
});

// Middleware B
app.Use(async (context, next) =>
{
    Console.WriteLine("B (before)");
    await next();               // va appeler le middleware suivant
    Console.WriteLine("B (after)");
});
app.Run();
