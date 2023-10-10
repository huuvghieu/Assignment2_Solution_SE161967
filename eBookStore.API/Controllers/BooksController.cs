using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{
    public class BooksController : ODataController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<BookResponseModel>>> Get()
        {
            var rs = await _bookService.GetBooks();
            return Ok(rs);
        }

        [EnableQuery]

        public async Task<ActionResult<BookResponseModel>> Get([FromRoute] int key)
        {
            var rs = await _bookService.GetBookById(key);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<BookResponseModel>> Post([FromBody] CreateBookRequestModel request)
        {
            var rs = await _bookService.InsertBook(request);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookResponseModel>> Put([FromRoute] int key, [FromBody] UpdateBookRequestModel request)
        {
            var rs = (await _bookService.UpdateBook(key, request));
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookResponseModel>> Delete([FromRoute] int key)
        {
            var rs = (await _bookService.DeleteBook(key));
            return Ok(rs);
        }
    }
}
