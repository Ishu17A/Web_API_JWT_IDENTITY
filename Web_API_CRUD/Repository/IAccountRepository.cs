using Microsoft.AspNetCore.Mvc;
using Web_API_CRUD.Models;

namespace Web_API_CRUD.Controllers
{
    public interface IAccountRepository
    {
        Task<LoginResponseModel> SignUp(Register model);
        Task<LoginResponseModel> Login(Login model);
    }
}
