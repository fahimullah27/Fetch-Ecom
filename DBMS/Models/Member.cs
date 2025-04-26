using System;
using System.Collections.Generic;

namespace DBMS.Models;

public partial class Member
{
    public int Memberid { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phonenumber { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
