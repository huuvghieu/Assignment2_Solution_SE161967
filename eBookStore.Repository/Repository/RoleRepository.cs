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
    public class RoleRepository
    {
        private static RoleRepository _instance;
        private static readonly object _instanceLock = new object();
        private static readonly EBookStoreDbContext _context = new EBookStoreDbContext();

        public RoleRepository() { }

        public static RoleRepository Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RoleRepository();
                    }
                    return _instance;
                }
            }
        }

        //get all
        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                return await _context.Role.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _context.Role.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
