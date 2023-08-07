using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.UserDomains.AppServices
{
    public interface IUserAppService
    {
        OperationResult<UserDTO> Create(UserDTO item);
        OperationResult<IEnumerable<UserDTO>> GetAllUsers();
        OperationResult<UserDTO> GetUserByID(int id);
        bool Find(UserDTO item);
        public OperationResult<UserDTO> Login(UserDTO item);
        public OperationResult<UserDTO> DeleteUser(int id);
        bool isMatched(string Password, byte[] PasswordHash, byte[] PasswordKey);
    }
}
