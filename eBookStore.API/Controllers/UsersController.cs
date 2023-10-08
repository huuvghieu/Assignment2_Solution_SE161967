using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{
    public class UsersController : ODataController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IQueryable<UserResponseModel>>> Get()
        {
            var rs = await _userService.GetUsers();
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseModel>> GetById([FromRoute] int key)
        {
            var rs = await _userService.GetUserById(key);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseModel>> Post([FromBody] CreateUserRequestModel request)
        {
            var rs = await _userService.InsertUser(request);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin,Member")]
        public async Task<ActionResult<UserResponseModel>> Put([FromRoute] int key, [FromBody] UpdateUserRequestModel request)
        {
            var rs = (await _userService.UpdateUser(key, request));
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseModel>> Delete([FromRoute] int key)
        {
            var rs = (await _userService.DeleteUser(key));
            return Ok(rs);
        }
    }
}
