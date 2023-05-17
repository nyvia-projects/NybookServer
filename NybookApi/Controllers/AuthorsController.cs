using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NybookApi.Dtos;
using NybookModel;

namespace NybookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly NybooksContext _context;

        public AuthorsController(NybooksContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            List<Author> authors = await _context.Authors.ToListAsync();
            return authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/Works/5
        [HttpGet("Works/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorWorks(int id)
        {
            List<Book> authorBooks = await _context.Books.Where(b => b.AuthorId == id).ToListAsync();
            if (authorBooks == null || authorBooks.Count == 0)
            {
                return NotFound();
            }

            return authorBooks;
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id) => _context.Authors.Any(e => e.Id == id);
    }
}
