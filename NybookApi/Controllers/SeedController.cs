﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NybookModel;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
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
                Name = "J.K. Rowling",
                Age = 56,
                Rating = 5
            };
            _context.Authors.Add(author1);

            Author author2 = new Author
            {
                Name = "Stephen King",
                Age = 74,
                Rating = 4
            };
            _context.Authors.Add(author2);

            Author author3 = new Author
            {
                Name = "Agatha Christie",
                Age = 85,
                Rating = 4
            };
            _context.Authors.Add(author3);

            Author author4 = new Author
            {
                Name = "Harper Lee",
                Age = 89,
                Rating = 3
            };
            _context.Authors.Add(author4);

            Author author5 = new Author
            {
                Name = "George Orwell",
                Age = 46,
                Rating = 5
            };
            _context.Authors.Add(author5);

            await _context.SaveChangesAsync();

            // Create books
            Book book1 = new Book
            {
                Title = "Harry Potter and the Philosopher's Stone",
                Year = 1997,
                Rating = 4,
                AuthorId = author1.Id,
                Author = null // Set the Author property to null to avoid circular reference
            };
            _context.Books.Add(book1);


            Book book2 = new Book
            {
                Title = "The Shining",
                Year = 1977,
                Rating = 4,
                AuthorId = author2.Id,
                Author = null // Set the Author property to null to avoid circular reference
            };
            _context.Books.Add(book2);
            addedBookList.Add(book2);

            Book book3 = new Book
            {
                Title = "Murder on the Orient Express",
                Year = 1934,
                Rating = 5,
                AuthorId = author3.Id,
                Author = null // Set the Author property to null to avoid circular reference
            };
            _context.Books.Add(book3);
            addedBookList.Add(book3);

            Book book4 = new Book
            {
                Title = "To Kill a Mockingbird",
                Year = 1960,
                Rating = 5,
                AuthorId = author4.Id,
                Author = null // Set the Author property to null to avoid circular reference
            };
            _context.Books.Add(book4);
            addedBookList.Add(book4);

            Book book5 = new Book
            {
                Title = "1984",
                Year = 1949,
                Rating = 5,
                AuthorId = author5.Id,
                Author = null // Set the Author property to null to avoid circular reference
            };
            _context.Books.Add(book5);
            addedBookList.Add(book5);

            Author author6 = new Author
            {
                Name = "Jane Austen",
                Age = 41,
                Rating = 4
            };
            _context.Authors.Add(author6);

            Author author7 = new Author
            {
                Name = "F. Scott Fitzgerald",
                Age = 44,
                Rating = 4
            };
            _context.Authors.Add(author7);

            Author author8 = new Author
            {
                Name = "Leo Tolstoy",
                Age = 82,
                Rating = 5
            };
            _context.Authors.Add(author8);

            await _context.SaveChangesAsync();

            Book book6 = new Book
            {
                Title = "Pride and Prejudice",
                Year = 1813,
                Rating = 4,
                AuthorId = author6.Id,
                Author = null
            };
            _context.Books.Add(book6);
            addedBookList.Add(book6);

            Book book7 = new Book
            {
                Title = "The Great Gatsby",
                Year = 1925,
                Rating = 5,
                AuthorId = author7.Id,
                Author = null
            };
            _context.Books.Add(book7);
            addedBookList.Add(book7);

            Book book8 = new Book
            {
                Title = "War and Peace",
                Year = 1869,
                Rating = 5,
                AuthorId = author8.Id,
                Author = null
            };
            _context.Books.Add(book8);
            addedBookList.Add(book8);

            Book book9 = new Book
            {
                Title = "Harry Potter and the Chamber of Secrets",
                Year = 1998,
                Rating = 5,
                AuthorId = author1.Id,
                Author = null
            };
            _context.Books.Add(book9);
            addedBookList.Add(book9);

            Book book10 = new Book
            {
                Title = "Harry Potter and the Prisoner of Azkaban",
                Year = 1999,
                Rating = 5,
                AuthorId = author1.Id,
                Author = null
            };
            _context.Books.Add(book10);
            addedBookList.Add(book10);

            Book book11 = new Book
            {
                Title = "It",
                Year = 1986,
                Rating = 5,
                AuthorId = author2.Id,
                Author = null
            };
            _context.Books.Add(book11);
            addedBookList.Add(book11);

            Book book12 = new Book
            {
                Title = "The Stand",
                Year = 1978,
                Rating = 4,
                AuthorId = author2.Id,
                Author = null
            };
            _context.Books.Add(book12);
            addedBookList.Add(book12);

            Book book13 = new Book
            {
                Title = "And Then There Were None",
                Year = 1939,
                Rating = 5,
                AuthorId = author3.Id,
                Author = null
            };
            _context.Books.Add(book13);
            addedBookList.Add(book13);

            Book book14 = new Book
            {
                Title = "The Murder of Roger Ackroyd",
                Year = 1926,
                Rating = 4,
                AuthorId = author3.Id,
                Author = null
            };
            _context.Books.Add(book14);
            addedBookList.Add(book14);

            Book book15 = new Book
            {
                Title = "Go Set a Watchman",
                Year = 2015,
                Rating = 3,
                AuthorId = author4.Id,
                Author = null
            };
            _context.Books.Add(book15);
            addedBookList.Add(book15);

            Book book16 = new Book
            {
                Title = "Animal Farm",
                Year = 1945,
                Rating = 4,
                AuthorId = author5.Id,
                Author = null
            };
            _context.Books.Add(book16);
            addedBookList.Add(book16);

            await _context.SaveChangesAsync();

            var serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(new
            {
                addedBookList.Count,
                Books = addedBookList
            }, serializerOptions);

            return Content(json, "application/json");
        }


    }

}
