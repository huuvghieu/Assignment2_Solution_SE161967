using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Repository
{
    public class BookRepository
    {
        private static BookRepository _instance;
        private static readonly object _instanceLock = new object();
        private static readonly EBookStoreDbContext _context = new EBookStoreDbContext();

        public BookRepository() { }

        public static BookRepository Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new BookRepository();
                    }
                    return _instance;
                }
            }
        }

        //get all
        public async Task<IEnumerable<Book>> GetBooks()
        {
            try
            {
                return await _context.Book.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            try
            {
                return _context.Book.AsNoTracking().Include(x => x.Publisher)
                                                   .Include(x => x.BookAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //get by id
        public async Task<Book> GetBookById(int id)
        {
            try
            {
                var book = await _context.Book.AsNoTracking().Where(x => x.BookId == id).SingleOrDefaultAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //insert
        public async Task InsertBook(Book book)
        {
            try
            {
                await _context.Book.AddAsync(book);
                _context.SaveChanges();
                _context.Entry<Book>(book).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //update
        public async Task UpdateBook(Book book)
        {
            try
            {
                _context.Entry<Book>(book).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry<Book>(book).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete
        public async Task DeleteBook(Book book)
        {
            try
            {
                _context.Book.Remove(book);
                _context.SaveChanges();
                _context.Entry<Book>(book).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

