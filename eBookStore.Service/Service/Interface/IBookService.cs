using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IBookService
    {
        public Task<IEnumerable<BookResponseModel>> GetBooks();

        public Task<BookResponseModel> GetBookById(int id);

        public Task<BookResponseModel> InsertBook(CreateBookRequestModel bookRequest);

        public Task<BookResponseModel> UpdateBook(int bookId, UpdateBookRequestModel bookRequest);

        public Task<BookResponseModel> DeleteBook(int bookId);


    }
}
