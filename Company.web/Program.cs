using Comapny.Repository.Interfaces;
using Comapny.Repository.Repository;
using Company.Data.Context;
using Company.Data.Entites;
using Company.Services.Interfaces;
using Company.Services.Mapping;
using Company.Services.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Company.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService,DepartmentService>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));

            builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config => 
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 6;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<CompanyDBContext>().AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                //TO avoid XSS Attacks 
                //Then run only on the http request
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                //Reset Expiration time but need user to be active if not active that don't be reset
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "Cookies";
                //Can Send Request For Https only
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //To Avoid CRST Attack
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
