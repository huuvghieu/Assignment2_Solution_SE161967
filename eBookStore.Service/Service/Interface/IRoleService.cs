using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IRoleService
    {
        public Task<IEnumerable<RoleResponseModel>> GetRoles();
    }
}
