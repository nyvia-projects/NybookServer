# Nybook Server for COMP 584

Nybook is full stack application that allows authenticated user to register authors, books and assign books to their respective authors.

**Nybook Server** is configured to work with **SQL Server**. It has 2 Models with one-to-many relationship.

Attributes of models and endpoints can be checked in **Swagger** by navigating to https://localhost:7064/swagger/index.html

Server also enforces authorization and authentication using **JWT tokens**.

---

### Here is a quick documentation for Author and Book Controllers

## AuthorsController

### Get Authors

- Route: GET /api/Authors
- Method: GET
- Description: Retrieves a list of authors.
- Authorization: Requires authentication.
- Returns:
  - 200 OK: List of AuthorDto objects representing authors.
  - 401 Unauthorized: If the request is not authenticated.

### Get Author

- Route: GET /api/Authors/{id}
- Method: GET
- Description: Retrieves a specific author by ID.
- Parameters:
  - id (int): The ID of the author to retrieve.
- Returns:
  - 200 OK: The AuthorDto object representing the author.
  - 404 Not Found: If the author with the specified ID does not exist.

### Get Author Works

- Route: GET /api/Authors/Works/{id}
- Method: GET
- Description: Retrieves a list of books written by a specific author.
- Parameters:
  - id (int): The ID of the author whose books to retrieve.
- Returns:
  - 200 OK: List of BookDto objects representing the author's books.
  - 404 Not Found: If the author has no books or does not exist.

### Post Author

- Route: POST /api/Authors
- Method: POST
- Description: Creates a new author.
- Body: The AuthorDto object representing the author to create.
- Returns:
  - 201 Created: The created AuthorDto object.
  - 400 Bad Request: If the request body is invalid.

### Put Author

- Route: PUT /api/Authors/{id}
- Method: PUT
- Description: Updates an existing author by ID.
- Parameters:
  - id (int): The ID of the author to update.
- Body: The AuthorDto object representing the updated author.
- Returns:
  - 204 No Content: If the update is successful.
  - 400 Bad Request: If the request body is invalid.
  - 404 Not Found: If the author with the specified ID does not exist.

### Delete Author

- Route: DELETE /api/Authors/{id}
- Method: DELETE
- Description: Deletes an existing author by ID.
- Parameters:
  - id (int): The ID of the author to delete.
- Returns:
  - 204 No Content: If the deletion is successful.
  - 404 Not Found: If the author with the specified ID does not exist.

## BooksController

### Get Books

- Route: GET /api/Books
- Method: GET
- Description: Retrieves a list of books.
- Authorization: Requires authentication.
- Returns:
  - 200 OK: List of BookDto objects representing books.
  - 401 Unauthorized: If the request is not authenticated.

### Get Book

- Route: GET /api/Books/{id}
- Method: GET
- Description: Retrieves a specific book by ID.
- Parameters:
  - id (int): The ID of the book to retrieve.
- Returns:
  - 200 OK: The BookDto object representing the book.
  - 404 Not Found: If the book with the specified ID does not exist.

### Put Book

- Route: PUT /api/Books/{id}
- Method: PUT
- Description: Updates an existing book by ID.
- Parameters:
  - id (int): The ID of the book to update.
  - Body: The BookDto object representing the updated book.
- Returns:
  - 204 No Content: If the update is successful.
  - 400 Bad Request

### Post Book

- Route: POST /api/Books
- Method: POST
- Description: Creates a new book.
- Body: The BookDto object representing the book to create.
- Returns:
  - 201 Created: The created BookDto object.
  - 400 Bad Request: If the request body is invalid.

### Delete Book

- Route: DELETE /api/Books/{id}
- Method: DELETE
- Description: Deletes an existing book by ID.
- Parameters:
  - id (int): The ID of the book to delete.
- Returns:
  - 204 No Content: If the deletion is successful.
  - 404 Not Found: If the book with the specified ID does not exist.
