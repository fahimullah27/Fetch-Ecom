using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBMS.Models;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=ep-noisy-tooth-a4mxzvj2-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_T6DuzO3ogUMt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Bookid).HasName("book_pkey");

            entity.ToTable("book");

            entity.HasIndex(e => e.Isbn, "book_isbn_key").IsUnique();

            entity.Property(e => e.Bookid)
                .ValueGeneratedNever()
                .HasColumnName("bookid");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Loanid).HasName("loan_pkey");

            entity.ToTable("loan");

            entity.Property(e => e.Loanid)
                .ValueGeneratedNever()
                .HasColumnName("loanid");
            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Loandate).HasColumnName("loandate");
            entity.Property(e => e.Memberid).HasColumnName("memberid");
            entity.Property(e => e.Returndate).HasColumnName("returndate");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.Bookid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("loan_bookid_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("loan_memberid_fkey");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Memberid).HasName("member_pkey");

            entity.ToTable("member");

            entity.HasIndex(e => e.Phonenumber, "member_phonenumber_key").IsUnique();

            entity.Property(e => e.Memberid)
                .ValueGeneratedNever()
                .HasColumnName("memberid");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(15)
                .HasColumnName("phonenumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
