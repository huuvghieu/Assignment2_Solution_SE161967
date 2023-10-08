using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IPublisherService
    {
        public Task<IEnumerable<PublisherResponseModel>> GetPublishers();

        public Task<PublisherResponseModel> GetPublisherById(int pubId);

        public Task<PublisherResponseModel> InsertPublisher(CreatePublisherRequestModel pubRequest);

        public Task<PublisherResponseModel> UpdatePublisher(int pubId, UpdatePublisherRequestModel pubRequest);

        public Task<PublisherResponseModel> DeletePublisher(int pubId);

    }
}
