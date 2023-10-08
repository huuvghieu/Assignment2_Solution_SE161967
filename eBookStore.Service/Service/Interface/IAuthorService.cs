using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorResponseModel>> GetAuthors();

        public Task<AuthorResponseModel> GetAuthorById(int id);

        public Task<AuthorResponseModel> InsertAuthor(CreateAuthorRequestModel authorRequest);

        public Task<AuthorResponseModel> UpdateAuthor(int authorId, UpdateAuthorRequestModel authorRequest);

        public Task<AuthorResponseModel> DeleteAuthor(int authorId);

    }
}
