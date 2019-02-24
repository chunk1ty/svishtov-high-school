using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SvishtovHighSchool.ReadModel;
using SvishtovHighSchool.ReadModel.Contracts;
using SvishtovHighSchool.Web.Models;

namespace SvishtovHighSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public HomeController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var r = await _courseRepository.GetAllAsync(); 

            return View();
        }

        [HttpPost]
        public IActionResult Index(CourseViewModel course)
        {
            return null;
        }
    }
}
