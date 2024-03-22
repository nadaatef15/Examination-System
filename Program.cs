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
			builder.Services.AddScoped<IRepoStudent, RepoStudent>();
            builder.Services.AddScoped<IRepoTrack, RepoTrack>();
            builder.Services.AddScoped<IRepoStudentCourse, RepoStudentCourse>();
            builder.Services.AddTransient<IRepoCourse, RepoCourse>();
            builder.Services.AddTransient<IRepoInstructor, RepoInstructor>();
            builder.Services.AddTransient<IRepoExam, RepoExam>();

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
				pattern: "{controller=Login}/{action=Show}/{id?}");

			app.Run();
		}
	}
}
