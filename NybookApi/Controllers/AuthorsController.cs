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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Age = a.Age,
                    Rating = a.Rating
                })
                .ToListAsync();
            return authors;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Age = a.Age,
                    Rating = a.Rating
                })
                .SingleOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpGet("Works/{id}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAuthorWorks(int id)
        {
            var authorBooks = await _context.Books
                .Where(b => b.AuthorId == id)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Rating = b.Rating,
                    AuthorId = b.AuthorId
                })
                .ToListAsync();

            if (!authorBooks.Any())
            {
                return NotFound();
            }

            return authorBooks;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Age = authorDto.Age,
                Rating = authorDto.Rating
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Age = author.Age,
                Rating = author.Rating
            });
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
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
