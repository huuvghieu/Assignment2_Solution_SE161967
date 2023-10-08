using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{
    public class RolesController : ODataController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IQueryable<RoleResponseModel>>> Get()
        {
            var rs = await _roleService.GetRoles();
            return Ok(rs);
        }
    }
}
