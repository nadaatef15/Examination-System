using Exam_System.Controllers.Filters;
using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Exam_System
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ExaminationContext>();
            builder.Services.AddScoped<IExaminationContextProcedures, ExaminationContextProcedures>();
            builder.Services.AddScoped<StudentIRepo, StudentRepo>();
            builder.Services.AddScoped<TrackIRepo, TrackRepo>();
            builder.Services.AddScoped<StudentCourseIRepo, StudentCourseRepo>();
            builder.Services.AddTransient<ICourseRepo, CourseRepo>();
            builder.Services.AddTransient<IInstructorRepo, InstructorRepo>();
            builder.Services.AddScoped<IInstructorAdminRepo, InstructorAdminRepo>();
            builder.Services.AddScoped<IAuthRepo, AuthRepo>();
            builder.Services.AddScoped<IInstructorCourseRepo, InstructorCourseRepo>();
            
            //applay filter to all
           /* builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });*/

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(a =>
            {
                a.LoginPath = "/Account/Login";
                a.AccessDeniedPath = "/Account/AccessError";

            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=instructor}/{action=index}/{id?}");

            app.Run();
        }
    }
}
