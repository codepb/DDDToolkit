using DDDToolkit.Repository.Sql;
using DDDToolkit.Samples.Library.Domain;

namespace DDDToolkit.Samples.Library.Repository.Sql
{
    public class BookRepository : Repository<Book, int, LibraryContext>
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}
