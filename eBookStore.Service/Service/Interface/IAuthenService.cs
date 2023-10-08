using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Interface
{
    public interface IAuthenService
    {
        public Task<LoginResponseModel> Login(LoginRequestModel loginRequest);

        public Task<UserResponseModel> Register(CreateUserRequestModel createMemberRequest);
    }
}
