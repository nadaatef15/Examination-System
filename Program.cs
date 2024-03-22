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
			builder.Services.AddScoped<IRepoStudent, RepoStudent>();
            builder.Services.AddScoped<IRepoTrack, RepoTrack>();
            builder.Services.AddScoped<IRepoStudentCourse, RepoStudentCourse>();
            builder.Services.AddTransient<IRepoCourse, RepoCourse>();
            builder.Services.AddTransient<IRepoInstructor, RepoInstructor>();
            builder.Services.AddTransient<IRepoExam, RepoExam>();
            builder.Services.AddScoped<IExaminationContextProcedures, ExaminationContextProcedures>();
            builder.Services.AddScoped<IInstructorAdminRepo, InstructorAdminRepo>();
            builder.Services.AddTransient<IInstructorRepo, InstructorRepo>();
            builder.Services.AddScoped<IInstructorCourseRepo, InstructorCourseRepo>();
            builder.Services.AddScoped<IAuthRepo, AuthRepo>();

            //applay filter to all
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });

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
                pattern: "{controller=Account}/{action=Show}/{id?}");

            app.Run();
        }
    }
}
