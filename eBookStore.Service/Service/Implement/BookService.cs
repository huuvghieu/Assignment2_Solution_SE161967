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
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;

        public BookService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BookResponseModel> DeleteBook(int bookId)
        {
            try
            {
                Book book = BookRepository.Instance.GetAll().Where(x => x.BookId == bookId)
                                                           .SingleOrDefault();
                if (book == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Not found book with id!!!", bookId.ToString());
                }
                await BookRepository.Instance.DeleteBook(book);
                return _mapper.Map<Book, BookResponseModel>(book);

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

        public async Task<BookResponseModel> GetBookById(int id)
        {
            try
            {
                var book = await BookRepository.Instance.GetBookById(id);
                if(book == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found book with {id}", id.ToString());
                }
                return _mapper.Map<Book ,BookResponseModel>(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BookResponseModel>> GetBooks()
        {
            try
            {
                var books = BookRepository.Instance.GetAll();
                List<BookResponseModel> result = new List<BookResponseModel>();
                foreach (var book in books)
                {
                    var bookAuthors = _mapper.Map<List<BookAuthorResponseModel>>(book.BookAuthors);
                    var publisherResult = _mapper.Map<Publisher, PublisherResponseModel>(book.Publisher);
                    var bookResult = new BookResponseModel
                    {
                        BookId = book.BookId,
                        Note = book.Note,
                        Price = book.Price,
                        PublishedDate = book.PublishedDate,
                        Advance = book.Advance,
                        PublishserId = book.PublishserId,
                        Royalty = book.Royalty,
                        Title = book.Title,
                        Type = book.Type,
                        YTDSale = book.YTDSale,
                        BookAuthors = bookAuthors
                    };
                    result.Add(bookResult);
                }
                return result;
            }
            catch (CrudException ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get all books failed!!!", ex.Message);
            }
        }

        public async Task<BookResponseModel> InsertBook(CreateBookRequestModel bookRequest)
        {
            try
            {
                Book book = new Book();
                _mapper.Map<CreateBookRequestModel, Book>(bookRequest, book);

                var checkBook = BookRepository.Instance.GetAll().Where(x => x.Title.Equals(bookRequest.Title))
                                                             .SingleOrDefault();
                if (checkBook != null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Book is already exist!!!", bookRequest.Title);
                }

                #region BookAuthor
                List<BookAuthor> bookAuthors = new List<BookAuthor>();
                Author author = new Author();
                
                foreach (var bookAuthorRequest in bookRequest.BookAuthors)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    _mapper.Map<BookAuthorRequestModel, BookAuthor>(bookAuthorRequest, bookAuthor);
                    bookAuthor.BookId = book.BookId;
                    bookAuthors.Add(bookAuthor);
                    author = AuthorRepository.Instance.GetAll().Where(x => x.AuthorId == bookAuthor.AuthorId)
                                                             .SingleOrDefault();
                    if (author == null)
                    {
                        throw new CrudException(HttpStatusCode.NotFound, $"Not found author with {bookAuthor.AuthorId}!!!", bookAuthor.AuthorId.ToString());
                    }
                    author.BookAuthors = bookAuthors;
                    book.BookAuthors = bookAuthors;
                }
                #endregion
                 await BookRepository.Instance.InsertBook(book);
                await AuthorRepository.Instance.UpdateAuthor(author);
                var bookAuthorResult = _mapper.Map<List<BookAuthorResponseModel>>(book.BookAuthors);
                var bookResult = new BookResponseModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Type = book.Type,
                    PublishserId = book.PublishserId,
                    Advance = book.Advance,
                    Note = book.Note,
                    Price = book.Price,
                    PublishedDate = book.PublishedDate,
                    Royalty = book.Royalty,
                    YTDSale = book.YTDSale,
                    BookAuthors = bookAuthorResult
                };

                return bookResult;
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

        public async Task<BookResponseModel> UpdateBook(int bookId, UpdateBookRequestModel bookRequest)
        {
            try
            {
                Book book = null;
                book = BookRepository.Instance.GetAll().Where(x => x.BookId == bookId)
                                             .SingleOrDefault();
                if (book == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found book with {bookId}!!!", bookId.ToString());
                }
                _mapper.Map<UpdateBookRequestModel, Book>(bookRequest, book);
                await BookRepository.Instance.UpdateBook(book);
                return _mapper.Map<Book, BookResponseModel>(book);
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
