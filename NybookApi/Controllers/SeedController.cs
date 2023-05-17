using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NybookModel;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NybookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly UserManager<NybooksUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly NybooksContext _context;

        public SeedController(UserManager<NybooksUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, NybooksContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("Users")]
        public async Task<IActionResult> ImportUsers()
        {
            const string roleUser = "RegisteredUser";
            const string roleAdmin = "Administrator";

            if (await _roleManager.FindByNameAsync(roleUser) is null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleUser));
            }
            if (await _roleManager.FindByNameAsync(roleAdmin) is null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleAdmin));
            }

            List<NybooksUser> addedUserList = new();
            (string name, string email) = ("admin", "admin@email.com");

            if (await _userManager.FindByNameAsync(name) is null)
            {
                NybooksUser userAdmin = new()
                {
                    UserName = name,
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.CreateAsync(userAdmin, _configuration["DefaultPasswords:Administrator"]
                    ?? throw new InvalidOperationException());
                await _userManager.AddToRolesAsync(userAdmin, new[] { roleUser, roleAdmin });
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
                addedUserList.Add(userAdmin);
            }

            (string name, string email) registered = ("user", "user@email.com");

            if (await _userManager.FindByNameAsync(registered.name) is null)
            {
                NybooksUser user = new()
                {
                    UserName = registered.name,
                    Email = registered.email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.CreateAsync(user, _configuration["DefaultPasswords:RegisteredUser"]
                    ?? throw new InvalidOperationException());
                await _userManager.AddToRoleAsync(user, roleUser);
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                addedUserList.Add(user);
            }

            if (addedUserList.Count > 0)
            {
                await _context.SaveChangesAsync();
            }

            // Seed quotes and authors
            // await ImportAuthorsAndBooks();

            return new JsonResult(new
            {
                addedUserList.Count,
                Users = addedUserList
            });
        }

        [HttpPost("AuthorsAndBooks")]
        public async Task<IActionResult> ImportAuthorsAndBooks()
        {
            // Create a list to store added books
            List<Book> addedBookList = new List<Book>();

            // Create authors
            Author author1 = new Author
            {
                Name = "Author 1",
                Age = 30,
                Rating = 5
            };
            _context.Authors.Add(author1);

            Author author2 = new Author
            {
                Name = "Author 2",
                Age = 35,
                Rating = 4
            };
            _context.Authors.Add(author2);

            Author author3 = new Author
            {
                Name = "Author 3",
                Age = 40,
                Rating = 3
            };
            _context.Authors.Add(author3);

            Author author4 = new Author
            {
                Name = "Author 4",
                Age = 45,
                Rating = 2
            };
            _context.Authors.Add(author4);

            Author author5 = new Author
            {
                Name = "Author 5",
                Age = 50,
                Rating = 1
            };
            _context.Authors.Add(author5);

            await _context.SaveChangesAsync();

            // Create books
            Book book1 = new Book
            {
                Title = "Book 1",
                Year = 2021,
                Rating = 4,
                AuthorId = author1.Id
            };
            _context.Books.Add(book1);
            addedBookList.Add(book1);

            Book book2 = new Book
            {
                Title = "Book 2",
                Year = 2019,
                Rating = 3,
                AuthorId = author2.Id
            };
            _context.Books.Add(book2);
            addedBookList.Add(book2);

            Book book3 = new Book
            {
                Title = "Book 3",
                Year = 2017,
                Rating = 5,
                AuthorId = author3.Id
            };
            _context.Books.Add(book3);
            addedBookList.Add(book3);

            Book book4 = new Book
            {
                Title = "Book 4",
                Year = 2015,
                Rating = 2,
                AuthorId = author4.Id
            };
            _context.Books.Add(book4);
            addedBookList.Add(book4);

            Book book5 = new Book
            {
                Title = "Book 5",
                Year = 2013,
                Rating = 1,
                AuthorId = author5.Id
            };
            _context.Books.Add(book5);
            addedBookList.Add(book5);

            await _context.SaveChangesAsync();

            return new JsonResult(new
            {
                addedBookList.Count,
                Books = addedBookList
            });

        }

    }

}