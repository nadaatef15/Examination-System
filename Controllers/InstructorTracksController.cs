using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Controllers
{

    public class InstructorTracksController : Controller
    {
        IInstructorAdminRepo instructorAdminRepo;
        IRepoTrack trackRepo;

        ExaminationContext db;

        public InstructorTracksController(IInstructorAdminRepo _instructorAdminRepo, IRepoTrack _trackIRepo, ExaminationContext _db)
        {
            instructorAdminRepo = _instructorAdminRepo;
            trackRepo = _trackIRepo;
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowTracks(int id)
        {
            var insTracks = instructorAdminRepo.GetTracksByInstructorId(id);
            ViewBag.InstructorId = id;
            return View("showTracks", insTracks);
        }

/*        public IActionResult DeleteTracks(int trackId, int insId)
        {
            var track = db.Tracks.Include(a => a.Courses).FirstOrDefault(a => a.TrackId == trackId);
            if (track.Courses.Count >0) {
                ViewBag.ErrorMsg = "this Track has cources";
            }
            else
            {
            trackRepo.Delete(trackId);
            }
            return ShowTracks(insId);
        }*/

    }
}
