using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Service.Service.Implement;
using eBookStore.Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.API.Controllers
{
    public class PublishersController : ODataController
    {
        private readonly IPublisherService _pubService;

        public PublishersController(IPublisherService pubService)
        {
            _pubService = pubService;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<PublisherResponseModel>>> Get()
        {
            var rs = await _pubService.GetPublishers();
            return Ok(rs);
        }

        [EnableQuery]
        public async Task<ActionResult<PublisherResponseModel>> Get([FromRoute] int key)
        {
            var rs = await _pubService.GetPublisherById(key);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<PublisherResponseModel>> Post([FromBody] CreatePublisherRequestModel request)
        {
            var rs = await _pubService.InsertPublisher(request);
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<PublisherResponseModel>> Put([FromRoute] int key, [FromBody] UpdatePublisherRequestModel request)
        {
            var rs = (await _pubService.UpdatePublisher(key, request));
            return Ok(rs);
        }

        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PublisherResponseModel>> Delete([FromRoute] int key)
        {
            var rs = (await _pubService.DeletePublisher(key));
            return Ok(rs);
        }
    }
}
