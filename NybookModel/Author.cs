using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NybookModel;

[Table("Author")]
public partial class Author
{

    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("age")]
    public int Age { get; set; }

    [Column("rating")]
    public int Rating { get; set; }


    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}