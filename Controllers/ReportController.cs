using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult showReport()
        {
            return View();
        }
    }
}
