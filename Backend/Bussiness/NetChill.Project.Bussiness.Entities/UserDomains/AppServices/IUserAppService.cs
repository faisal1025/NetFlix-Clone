using NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.UserDomains.AppServices
{
    public interface IUserAppService
    {
        OperationResult<UserDTO> Create(UserDTO item);
        OperationResult<IEnumerable<UserDTO>> GetAllUsers();
        OperationResult<UserDTO> GetUserByID(int id);
        UserDTO GetUserByEmail(string email);
        bool Find(UserDTO item);
        public OperationResult<UserDTO> Login(UserDTO item);
        public OperationResult<UserDTO> DeleteUser(int id);
        bool isMatched(string Password, byte[] PasswordHash, byte[] PasswordKey);
        Task<OperationResult<UserDTO>> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
