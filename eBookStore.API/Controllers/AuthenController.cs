using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{

    [Route("odata/authen")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel request)
        {
            var rs = await _authenService.Login(request);
            return Ok(rs);
        }

        [HttpPost("registeration")]
        public async Task<ActionResult<UserResponseModel>> Register([FromBody] CreateUserRequestModel request)
        {
            var rs = await _authenService.Register(request);
            return Ok(rs);
        }
    }
}
