using AutoMapper;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.RequestModel;
using eBookStore.Repository.Model.ResponseModel;
using eBookStore.Repository.Repository;
using eBookStore.Service.Exceptions;
using eBookStore.Service.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Service.Service.Implement
{
    public class AuthenService : IAuthenService
    {
        private readonly IMapper _mapper;
        private string _secretKey;
        private IConfiguration _config;

        public AuthenService(IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
        }
        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            try
            {
                //check admin
                var admin = _config.GetSection("Admin");
                var emailAdmin = admin["email"];
                var passwordAdmin = admin["password"];
                var listRole = RoleRepository.Instance.GetAll();

                bool isAdmin = loginRequest.Email.Equals(emailAdmin) && loginRequest.Password.Equals(passwordAdmin);
                #region Admin
                if (isAdmin)
                {
                    var roleAdmin = listRole.Where(x => x.RoleId == 1).SingleOrDefault();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var secretKey = _config.GetSection("ApiSetting");
                    _secretKey = secretKey["Secret"];

                    var key = Encoding.ASCII.GetBytes(_secretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, emailAdmin),
                            new Claim(ClaimTypes.Role, roleAdmin.Description)
                        }),

                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    LoginResponseModel loginResponse = new LoginResponseModel
                    {
                        Token = tokenHandler.WriteToken(token),
                    };
                    return loginResponse;
                }
                #endregion
                #region Member
                else
                {
                    var roleMember = listRole.Where(x => x.RoleId == 2).SingleOrDefault();
                    var user = UserRepository.Instance.GetAll().Where(x => x.EmailAddres.Equals(loginRequest.Email) &&
                                                                               x.Password.Equals(loginRequest.Password)).SingleOrDefault();
                    if (user == null)
                    {
                        throw new CrudException(HttpStatusCode.NotFound, "Wrong Email or Password!!!", loginRequest.Email);
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var secretKey = _config.GetSection("ApiSetting");
                    _secretKey = secretKey["Secret"];

                    var key = Encoding.ASCII.GetBytes(_secretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, loginRequest.Email),
                            new Claim(ClaimTypes.Role, "Customer")
                        }),

                        Expires = DateTime.UtcNow.AddDays(7),

                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    LoginResponseModel loginResponse = new LoginResponseModel
                    {
                        Token = tokenHandler.WriteToken(token),
                    };

                    return loginResponse;
                }
                #endregion
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

        public async Task<UserResponseModel> Register(CreateUserRequestModel userRequest)
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
    }
}
