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
    public class UserRepository
    {
        private static UserRepository _instance;
        private static readonly object _instanceLock = new object();
        private static readonly EBookStoreDbContext _context = new EBookStoreDbContext();

        public UserRepository() { }

        public static UserRepository Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserRepository();
                    }
                    return _instance;
                }
            }
        }

        //get all
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _context.User.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _context.User.AsNoTracking().Include(x => x.Publisher)
                                                   .Include(x => x.Role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //get by id
        public async Task<User> GetUserById(int id)
        {
            try
            {
                var user = await _context.User.AsNoTracking().Where(x => x.UserId == id).SingleOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //insert
        public async Task InsertUser(User user)
        {
            try
            {
                await _context.User.AddAsync(user);
                _context.SaveChanges();
                _context.Entry<User>(user).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //update
        public async Task UpdateUser(User user)
        {
            try
            {
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry<User>(user).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete
        public async Task DeleteUser(User user)
        {
            try
            {
                _context.User.Remove(user);
                _context.SaveChanges();
                _context.Entry<User>(user).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
