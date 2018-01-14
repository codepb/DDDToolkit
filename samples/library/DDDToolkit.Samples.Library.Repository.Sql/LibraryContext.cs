using DDDToolkit.Samples.Library.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Samples.Library.Repository.Sql
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne(b => b.Author, a => 
            {
                a.Property(author => author.FirstName).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
                a.Property(author => author.LastName).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            });
            
            modelBuilder.Entity<Book>().OwnsOne(b => b.ISBN, i =>
            {
                i.Property(isbn => isbn.Value).UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            });
            
        }
    }
}
