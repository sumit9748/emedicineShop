using Emedicine.BAL.CartBased;
using Emedicine.BAL.MedcineBased;
using Emedicine.BAL.OrderBased;
using Emedicine.BAL.UserBased;
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

builder.Services.AddHttpClient();
//add injection from dal to program
builder.Services.AddScoped<IDataAccess, DataAccess>();

//add injection from business layer to dal
builder.Services.AddScoped<IUserManager,UserManager>();

//medicine scope
builder.Services.AddScoped<IMedicineMain, MedicineMain>();
//cart scope
builder.Services.AddScoped<ICartMain, CartMain>();
//order scope
builder.Services.AddScoped<IOrderMain, OrderMain>();



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
