using Books.Models;
using Books.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var books = _context.books.Include(m => m.Category).ToList();
            return View(books);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _context.books.Include(m => m.Category).SingleOrDefault(m => m.Id == id);
            if (book == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(book);
        }

        public ActionResult Create() 
        {
            var viewModel = new BookFormViewModel()
            {
               Categories = _context.categories.Where(m => m.IsActive).ToList(),
            };
             
            return View(viewModel);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _context.books.Find(id);
            if (book == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var viewModel = new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryId = book.CategoryId,
                Description = book.Description,
                Categories = _context.categories.Where(m => m.IsActive).ToList(),
            };

            return View("Create", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _context.categories.Where(m => m.IsActive).ToList();
                return View("Create", viewModel);
            }
            // To create a new book
           if(viewModel.Id == 0)
            {
                var book = new Book
                {
                    Title = viewModel.Title,
                    Author = viewModel.Author,
                    CategoryId = viewModel.CategoryId,
                    Description = viewModel.Description,
                };
                _context.books.Add(book);
            }
           else // To Update values
            {
                var book = _context.books.Find(viewModel.Id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                book.Title = viewModel.Title;
                book.Author = viewModel.Author;
                book.CategoryId = viewModel.CategoryId;
                book.Description = viewModel.Description;
            }


            _context.SaveChanges();
            TempData["Message"] = "Saved Successfully!";

            return RedirectToAction("Index");
        }


    }
}