
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP.KitStem.API.Data;
using SWP.KitStem.Service.BusinessModels;
using SWP.KitStem.Service.Services;
using SWP.KitStem.Repository;
using SWP.KitStem.Service.Utils;

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

            //Add config for required email
            builder.Services.Configure<IdentityOptions>(options =>
                options.SignIn.RequireConfirmedEmail = true);

            //Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            //Add Email Configs
            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);




            //builder.Services.AddDbContext<KitStemContext>(options =>
            //    options.UseSqlServer(connectionString,
            //        sqlOptions => sqlOptions.MigrationsAssembly("SWP.KitStem.Repository")));
            //builder.Services.AddCors(option =>
            //    option.AddPolicy("CORS", builder =>
            //        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            builder.Services.AddScoped<IEmailService, EmailService>();
            //builder.Services.AddScoped<Order>();
            //builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<ComponentService>();
            builder.Services.AddScoped<LevelService>();
            builder.Services.AddScoped<LabService>();
            builder.Services.AddScoped<LocalfileService>();
            builder.Services.AddScoped<KitImageService>();
            builder.Services.AddScoped<KitService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.Configure<LocalfileSettings>(builder.Configuration.GetSection("LocalfileSettings"));

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
