using Serilog;
using Microsoft.EntityFrameworkCore;
using HotelListning.Data;
using HotelListning.Configuration;
using HotelListning.Contracts;
using HotelListning.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("HotelListningDbConnetion");

builder.Services.AddDbContext<HotelListningDbContext>(options =>
{
    options.UseSqlServer(connectionstring);
});
// Add services to the container.

builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

