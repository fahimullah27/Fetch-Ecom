using System;
using System.Collections.Generic;

namespace DBMS.Models;

public partial class Book
{
    public int Bookid { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Isbn { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
