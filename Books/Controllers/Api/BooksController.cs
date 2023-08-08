using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Books.Controllers.Api
{
    public class BooksController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var book = _context.books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            _context.books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }
    }
}
