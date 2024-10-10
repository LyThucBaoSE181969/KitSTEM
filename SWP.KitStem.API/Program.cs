
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP.KitStem.API.Data;

namespace SWP.KitStem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //For Entity Framework
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            //For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            //Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            //builder.Services.AddDbContext<KitStemContext>(options =>
            //    options.UseSqlServer(connectionString,
            //        sqlOptions => sqlOptions.MigrationsAssembly("SWP.KitStem.Repository")));
            //builder.Services.AddCors(option =>
            //    option.AddPolicy("CORS", builder =>
            //        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            //builder.Services.AddScoped<Order>();
            //builder.Services.AddScoped<CartService>();
            //builder.Services.AddScoped<LabService>();
            //builder.Services.AddScoped<KitService>();
            //builder.Services.AddScoped<CategoryService>();
            //builder.Services.AddScoped<UnitOfWork>();

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
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<KitStemContext>();
            //    db.Database.Migrate();
            //}

            app.Run();
        }
    }
}
