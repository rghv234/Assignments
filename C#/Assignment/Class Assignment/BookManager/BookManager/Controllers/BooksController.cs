using System.Linq;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    public class BooksController : Controller
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", Price = 9.99m, PublicationYear = 1949 },
            new Book { Id = 2, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m, PublicationYear = 1925 },
        };

        public IActionResult Index()
        {
            return View(_books);
        }

        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = _books.Max(b => b.Id) + 1;
                _books.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}