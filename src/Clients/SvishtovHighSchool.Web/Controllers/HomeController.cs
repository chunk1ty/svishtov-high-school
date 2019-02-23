using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SvishtovHighSchool.ReadModel;
using SvishtovHighSchool.Web.Models;

namespace SvishtovHighSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookService _bookService;

        public HomeController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _bookService.Create(new CourseDto() {Name = "ank", AggregateId = "sdsds"});

            return View();
        }

        [HttpPost]
        public IActionResult Index(CourseViewModel course)
        {
            return null;
        }
    }

    public class BookService
    {
        private readonly IMongoCollection<CourseDto> _books;

        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SvishtovHighSchoolDb"));
            var database = client.GetDatabase("SvishtovHighSchoolDb");
            _books = database.GetCollection<CourseDto>("Courses");
        }

        public List<CourseDto> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public CourseDto Get(string id)
        {
            return _books.Find<CourseDto>(book => book.Id == id).FirstOrDefault();
        }

        public CourseDto Create(CourseDto book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, CourseDto bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(CourseDto bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }
    }
}
