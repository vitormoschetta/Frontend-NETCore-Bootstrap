using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> Register(UserRegister user);
        Task<UserResult> RegisterAdmin(UserRegister user);
        Task<UserResult> Login(User user);
        Task<List<User>> GetInactivesFirstAccess();
        Task<UserResult> ActivateFirstAccess(int id, string role = "User");
        Task<UserResult> Delete(int id);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByName(string name);
        Task<UserResult> UpdateRoleActive(User user);
        Task<UserResult> UpdatePassword(UserUpdatePassword userUpdatePassword);
        Task<List<User>> Search(string param);
    }
}