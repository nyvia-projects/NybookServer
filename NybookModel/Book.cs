using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NybookModel;

[Table("Book")]
public partial class Book
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(10)]
    public string Title { get; set; } = null!;

    [Column("year")]
    public int Year { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [Column("authorId")]
    public int AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; } = null!;
}
