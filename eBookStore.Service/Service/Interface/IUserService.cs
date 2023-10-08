using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IUserService
    {
        public Task<IEnumerable<UserResponseModel>> GetUsers();

        public Task<UserResponseModel> GetUserById(int id);

        public Task<UserResponseModel> InsertUser(CreateUserRequestModel userRequest);

        public Task<UserResponseModel> UpdateUser(int userId, UpdateUserRequestModel userRequest);

        public Task<UserResponseModel> DeleteUser(int userId);

    }
}
