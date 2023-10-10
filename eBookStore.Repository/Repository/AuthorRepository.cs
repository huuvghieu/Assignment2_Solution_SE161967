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
    public class AuthorRepository
    {
        private static AuthorRepository _instance;
        private static readonly object _instanceLock = new object();
        private static readonly EBookStoreDbContext _context = new EBookStoreDbContext();

        public AuthorRepository() { }

        public static AuthorRepository Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AuthorRepository();
                    }
                    return _instance;
                }
            }
        }

        //get all
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            try
            {
                return await _context.Author.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Author> GetAll()
        {
            try
            {
                return _context.Author.AsNoTracking().Include(x => x.BookAuthors);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //get by id
        public async Task<Author> GetAuthorById(int id)
        {
            try
            {
                var author = await _context.Author.AsNoTracking().Where(x => x.AuthorId == id).SingleOrDefaultAsync();
                return author;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //insert
        public async Task InsertAuthor(Author author)
        {
            try
            {
                await _context.Author.AddAsync(author);
                _context.SaveChanges();
                _context.Entry<Author>(author).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //update
        public async Task UpdateAuthor(Author author)
        {
            try
            {
                _context.Entry<Author>(author).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry<Author>(author).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete
        public async Task DeleteAuthor(Author author)
        {
            try
            {
                _context.Author.Remove(author);
                _context.SaveChanges();
                _context.Entry<Author>(author).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
