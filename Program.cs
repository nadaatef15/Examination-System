using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;

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
			builder.Services.AddScoped<StudentIRepo, StudentRepo>();
            builder.Services.AddScoped<TrackIRepo, TrackRepo>();
            builder.Services.AddScoped<StudentCourseIRepo, StudentCourseRepo>();
            builder.Services.AddScoped<CourseIRepo, CourseRepo>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
