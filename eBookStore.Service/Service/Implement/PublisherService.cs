using AutoMapper;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.RequestModel;
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
    public class PublisherService : IPublisherService
    {
        private readonly IMapper _mapper;
        public PublisherService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<PublisherResponseModel> DeletePublisher(int pubId)
        {
            try
            {
                Publisher publisher = PublisherRepository.Instance.GetAll().Where(x => x.PublisherId == pubId)
                                                           .SingleOrDefault();
                if (publisher == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Not found publisher with id!!!", pubId.ToString());
                }
                await PublisherRepository.Instance.DeletePublisher(publisher);
                return _mapper.Map<Publisher, PublisherResponseModel>(publisher);

            }
            catch (CrudException ex)
            {
                throw new CrudException(ex.StatusCode, ex.Message, ex?.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PublisherResponseModel> GetPublisherById(int pubId)
        {
            try
            {
                var publisher = await PublisherRepository.Instance.GetPublisherById(pubId);
                if (publisher == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found publisher with {pubId}", pubId.ToString());
                }
                return _mapper.Map<Publisher, PublisherResponseModel>(publisher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PublisherResponseModel>> GetPublishers()
        {
            try
            {
                var publishers = await PublisherRepository.Instance.GetPublishers();
                return _mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherResponseModel>>(publishers);
            }
            catch (CrudException ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get all publishers failed!!!", ex.Message);
            }
        }

        public async Task<PublisherResponseModel> InsertPublisher(CreatePublisherRequestModel pubRequest)
        {
            try
            {
                var checkPub = PublisherRepository.Instance.GetAll().Where(x => x.PublisherName.Equals(pubRequest.PublisherName))
                                                             .SingleOrDefault();
                if (checkPub != null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "publisher is already exist!!!", pubRequest.PublisherName);
                }
                var publisher = _mapper.Map<CreatePublisherRequestModel, Publisher>(pubRequest);
                await PublisherRepository.Instance.InsertPublisher(publisher);
                return _mapper.Map<Publisher, PublisherResponseModel>(publisher);
            }
            catch (CrudException ex)
            {
                throw new CrudException(ex.StatusCode, ex.Message, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PublisherResponseModel> UpdatePublisher(int pubId, UpdatePublisherRequestModel pubRequest)
        {
            try
            {
                Publisher publisher = null;
                publisher = PublisherRepository.Instance.GetAll().Where(x => x.PublisherId == pubId)
                                             .SingleOrDefault();
                if (publisher == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found publisher with {pubId}!!!", pubId.ToString());
                }
                _mapper.Map<UpdatePublisherRequestModel, Publisher>(pubRequest, publisher);
                await PublisherRepository.Instance.UpdatePublisher(publisher);
                return _mapper.Map<Publisher, PublisherResponseModel>(publisher);
            }
            catch (CrudException ex)
            {
                throw new CrudException(ex.StatusCode, ex.Message, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
