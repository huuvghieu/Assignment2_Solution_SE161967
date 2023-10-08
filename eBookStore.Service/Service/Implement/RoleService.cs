using AutoMapper;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Repository.Repository;
using eBookStore.Service.Exceptions;
using eBookStore.Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Implement
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        public RoleService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoleResponseModel>> GetRoles()
        {
            try
            {
                var roles = await RoleRepository.Instance.GetRoles();
                return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleResponseModel>>(roles);
            }
            catch (CrudException ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get all roles failed!!!", ex.Message);
            }
        }
    }
}
