using System;
using System.Collections.Generic;

namespace DBMS.Models;

public partial class Loan
{
    public int Loanid { get; set; }

    public int Bookid { get; set; }

    public int Memberid { get; set; }

    public DateOnly Loandate { get; set; }

    public DateOnly? Returndate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
