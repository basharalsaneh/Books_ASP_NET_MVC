using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Books.Models;
using Books.ViewModels;

namespace categorys.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var categories = _context.categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            var catagory = new Category();

            return View(catagory);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _context.categories.Find(id);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var UpdatedCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
                IsActive = category.IsActive,
            };

            return View("Create", UpdatedCategory);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _context.categories.Find(id);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    category.Name = _context.categories.Where(m => m.IsActive).ToList();
            //    return View("Create", category);
            //}

            // To create a new category
            if (obj.Id == 0)
            {
                var category = new Category
                {
                    Name = obj.Name,
                    IsActive = obj.IsActive,
                };
                _context.categories.Add(category);
            }
            else // To Update values
            {
                var category = _context.categories.Find(obj.Id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                category.Name = obj.Name;
                category.IsActive = obj.IsActive;
            }


            _context.SaveChanges();
            TempData["Message"] = "Saved Successfully!";

            return RedirectToAction("Index");
        }

    }
}