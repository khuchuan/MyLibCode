﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestEntityFw.Data.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}
