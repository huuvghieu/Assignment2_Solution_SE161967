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
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;

        public AuthorService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<AuthorResponseModel> DeleteAuthor(int authorId)
        {
            try
            {
                Author author = AuthorRepository.Instance.GetAll().Where(x => x.AuthorId == authorId)
                                                           .SingleOrDefault();
                if (author == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Not found author with id!!!", authorId.ToString());
                }
                await AuthorRepository.Instance.DeleteAuthor(author);
                return _mapper.Map<Author, AuthorResponseModel>(author);

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

        public async Task<AuthorResponseModel> GetAuthorById(int id)
        {
            try
            {
                var author = await AuthorRepository.Instance.GetAuthorById(id);
                if (author == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found author with {id}", id.ToString());
                }
                return _mapper.Map<Author, AuthorResponseModel>(author);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<AuthorResponseModel>> GetAuthors()
        {
            try
            {
                var authors = await AuthorRepository.Instance.GetAuthors();
                return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorResponseModel>>(authors);
            }
            catch (CrudException ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get all authors failed!!!", ex.Message);
            }
        }

        public async Task<AuthorResponseModel> InsertAuthor(CreateAuthorRequestModel authorRequest)
        {
            try
            {
                var checkAuthor = AuthorRepository.Instance.GetAll().Where(x => x.EmailAddress.Equals(authorRequest.EmailAddress))
                                                             .SingleOrDefault();
                if (checkAuthor != null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Author is already exist!!!", authorRequest.EmailAddress);
                }
                var author = _mapper.Map<CreateAuthorRequestModel, Author>(authorRequest);
                await AuthorRepository.Instance.InsertAuthor(author);
                return _mapper.Map<Author, AuthorResponseModel>(author);
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

        public async Task<AuthorResponseModel> UpdateAuthor(int authorId, UpdateAuthorRequestModel authorRequest)
        {
            try
            {
                Author author = null;
                author = AuthorRepository.Instance.GetAll().Where(x => x.AuthorId == authorId)
                                             .SingleOrDefault();
                if (author == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not author book with {authorId}!!!", authorId.ToString());
                }
                _mapper.Map<UpdateAuthorRequestModel, Author>(authorRequest, author);
                await AuthorRepository.Instance.UpdateAuthor(author);
                return _mapper.Map<Author, AuthorResponseModel>(author);
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
