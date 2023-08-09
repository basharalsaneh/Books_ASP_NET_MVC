using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace Books.Controllers.Api
{
    public class CategoryController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = _context.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
    }
}