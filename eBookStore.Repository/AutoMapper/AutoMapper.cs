using AutoMapper;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Book
            CreateMap<Book, BookResponseModel>().ReverseMap();
            CreateMap<CreateBookRequestModel, Book>();
            CreateMap<UpdateBookRequestModel, Book>();
            #endregion

            #region Author
            CreateMap<Author, AuthorResponseModel>().ReverseMap();
            CreateMap<CreateAuthorRequestModel, Author>();
            CreateMap<UpdateAuthorRequestModel, Author>();
            #endregion

            #region BookAuthor
            CreateMap<BookAuthor, BookAuthorResponseModel>().ReverseMap();
            CreateMap<BookAuthorRequestModel, BookAuthor>();
            #endregion

            #region Publisher
            CreateMap<Publisher, PublisherResponseModel>().ReverseMap();
            CreateMap<CreatePublisherRequestModel, Publisher>();
            CreateMap<UpdatePublisherRequestModel, Publisher>();
            #endregion

            #region User
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<CreateUserRequestModel, User>();
            CreateMap<UpdateUserRequestModel, User>();
            #endregion

            #region Role
            CreateMap<Role, RoleResponseModel>().ReverseMap();
            #endregion

        }
    }
}
