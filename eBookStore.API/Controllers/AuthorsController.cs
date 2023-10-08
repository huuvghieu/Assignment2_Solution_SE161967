using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{
    public class AuthorsController : ODataController
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<AuthorResponseModel>>> Get()
        {
            var rs = await _authorService.GetAuthors();
            return Ok(rs);
        }

        [EnableQuery]
        public async Task<ActionResult<AuthorResponseModel>> Get([FromRoute] int key)
        {
            var rs = await _authorService.GetAuthorById(key);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorResponseModel>> Post([FromBody] CreateAuthorRequestModel request)
        {
            var rs = await _authorService.InsertAuthor(request);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorResponseModel>> Put([FromRoute] int key, [FromBody] UpdateAuthorRequestModel request)
        {
            var rs = (await _authorService.UpdateAuthor(key, request));
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorResponseModel>> Delete([FromRoute] int key)
        {
            var rs = (await _authorService.DeleteAuthor(key));
            return Ok(rs);
        }
    }
}
