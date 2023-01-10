using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using ProjetWebAPI.Models.Outputs;
using ProjetWebAPI.Services.Interfaces;
using ProjetWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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


//JWT management
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware A
app.Use(async (context, next) =>
{
    Console.WriteLine("A (before)");
    await next();               // va appeler le middleware suivant
    Console.WriteLine("A (after)");
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
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
