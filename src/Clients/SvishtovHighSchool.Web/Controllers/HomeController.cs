using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel.Contracts;
using SvishtovHighSchool.Web.Models;

namespace SvishtovHighSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CourseViewModel course)
        {
            var courseCommand = new CreateCourseCommand(course.Name);

            await _mediator.Publish(courseCommand);
            

            return RedirectToAction("Index");
        }
    }
}
