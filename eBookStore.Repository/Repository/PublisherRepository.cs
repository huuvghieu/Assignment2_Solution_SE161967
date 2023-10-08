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
    public class PublisherRepository
    {
        private static PublisherRepository _instance;
        private static readonly object _instanceLock = new object();
        private static readonly EBookStoreDbContext _context = new EBookStoreDbContext();

        public PublisherRepository() { }

        public static PublisherRepository Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PublisherRepository();
                    }
                    return _instance;
                }
            }
        }

        //get all
        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            try
            {
                return await _context.Publisher.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Publisher> GetAll()
        {
            try
            {
                return _context.Publisher.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //get by id
        public async Task<Publisher> GetPublisherById(int id)
        {
            try
            {
                var publisher = await _context.Publisher.AsNoTracking().Where(x => x.PublisherId == id).SingleOrDefaultAsync();
                return publisher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //insert
        public async Task InsertPublisher(Publisher publisher)
        {
            try
            {
                await _context.Publisher.AddAsync(publisher);
                _context.SaveChanges();
                _context.Entry<Publisher>(publisher).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //update
        public async Task UpdatePublisher(Publisher publisher)
        {
            try
            {
                _context.Entry<Publisher>(publisher).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry<Publisher>(publisher).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete
        public async Task DeletePublisher(Publisher publisher)
        {
            try
            {
                _context.Publisher.Remove(publisher);
                _context.SaveChanges();
                _context.Entry<Publisher>(publisher).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
