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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<UserResponseModel> DeleteUser(int userId)
        {
            try
            {
                User user = UserRepository.Instance.GetAll().Where(x => x.UserId == userId)
                                                           .SingleOrDefault();
                if (user == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "Not found user with id!!!", userId.ToString());
                }
                await UserRepository.Instance.DeleteUser(user);
                return _mapper.Map<User, UserResponseModel>(user);

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

        public async Task<UserResponseModel> GetUserById(int id)
        {
            try
            {
                var user = await UserRepository.Instance.GetUserById(id);
                if (user == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found user with {id}", id.ToString());
                }
                return _mapper.Map<User, UserResponseModel>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserResponseModel>> GetUsers()
        {
            try
            {
                var users = await UserRepository.Instance.GetUsers();
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseModel>>(users);
            }
            catch (CrudException ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get all books failed!!!", ex.Message);
            }
        }

        public async Task<UserResponseModel> InsertUser(CreateUserRequestModel userRequest)
        {
            try
            {
                var check = UserRepository.Instance.GetAll().Where(x => x.EmailAddres.Equals(userRequest.EmailAddres))
                                                             .SingleOrDefault();
                if (check != null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, "User is already exist!!!", userRequest.EmailAddres);
                }
                var user = _mapper.Map<CreateUserRequestModel, User>(userRequest);
                await UserRepository.Instance.InsertUser(user);
                return _mapper.Map<User, UserResponseModel>(user);
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

        public async Task<UserResponseModel> UpdateUser(int userId, UpdateUserRequestModel userRequest)
        {
            try
            {
                User user = null;
                user = UserRepository.Instance.GetAll().Where(x => x.UserId == userId)
                                             .SingleOrDefault();
                if (user == null)
                {
                    throw new CrudException(HttpStatusCode.NotFound, $"Not found user with {userId}!!!", userId.ToString());
                }
                _mapper.Map<UpdateUserRequestModel, User>(userRequest, user);
                await UserRepository.Instance.UpdateUser(user);
                return _mapper.Map<User, UserResponseModel>(user);
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
