using LibraryManagementApplication.Data;
using LibraryManagementApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookDetailsController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;

        public BookDetailsController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/BookDetails
        /// Retrieve all books from the database.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public ActionResult<IEnumerable<BookDetails>> GetBooks()
        {
            return _context.Books.ToList();
        }

        /// <summary>
        /// GET: api/BookDetails/{id}
        ///   Retrieve a specific book by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public ActionResult<BookDetails> GetBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// POST: api/BookDetails
        /// Add a new book to the database.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<BookDetails> PostBook(BookDetails book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        /// <summary>
        /// PUT: api/BookDetails/{id}
        /// Update an existing book in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBook"></param>
        /// <returns></returns>        
       
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PutBook(int id, BookDetails updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;

            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE: api/BookDetails/{id}
        /// Delete a book from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
