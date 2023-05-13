
using Emedicine.BAL;
using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess;
using Emedicine.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//adding db context
builder.Services.AddDbContext<MedicineDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EMedcs"), b => b.MigrationsAssembly("Emedicine"));
});


//add injection from dal to program
builder.Services.AddScoped<IDataAccess, DataAccess>();

//add injection from business layer to dal
builder.Services.AddScoped<IMedicineManager,MedicineManager>();
// Add services to the container.

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

app.Run();
