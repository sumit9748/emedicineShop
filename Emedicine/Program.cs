using Emedicine.BAL.CartBased;
using Emedicine.BAL.MedcineBased;
using Emedicine.BAL.OrderBased;
using Emedicine.BAL.UserBased;
using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess;
using Emedicine.DAL.DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //adding db context
        builder.Services.AddDbContext<MedicineDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("EMedcs"), b => b.MigrationsAssembly("Emedicine"));
        });
        /*privar

        var appSettingsSection = Configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettingsSection);

        //JWT Authentication
        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Key);
        */


        builder.Services.AddHttpClient();
        //add injection from dal to program
        builder.Services.AddScoped<IDataAccess, DataAccess>();

        //add injection from business layer to dal
        builder.Services.AddScoped<IUserManager, UserManager>();

        //medicine scope
        builder.Services.AddScoped<IMedicineMain, MedicineMain>();
        //cart scope
        builder.Services.AddScoped<ICartMain, CartMain>();
        //order scope
        builder.Services.AddScoped<IOrderMain, OrderMain>();
        //add jwt authetication
        /*
        builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
        */


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sumit9748@"))
                   };
               });

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
        }

}