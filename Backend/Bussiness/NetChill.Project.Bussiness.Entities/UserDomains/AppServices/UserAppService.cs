using AutoMapper;
using Company.Project.Core.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NetChill.Project.Bussiness.Entities.UserDomains.UoW;
using NetChill.Project.Bussiness.Entities.UserDomains.Repository;
using NetChil.Project.Foundation.Core.ExceptionManagement;
using NetChill.Project.UserDomains.AppServices.DTOs;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.DataAccess.Data.UoW;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using System.Security.Cryptography;
using NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs;
using System.Threading.Tasks;

namespace NetChill.Project.UserDomains.AppServices
{
    public class UserAppService : IUserAppService
    {
        private IMapper mapper;
        private IUserUnitOfWork unitOfWork;
        //private IExceptionManager exceptionManager;
        private IUserRepository UserRepository;
        private readonly IExceptionManager exceptionManager;

        public UserAppService(IUserUnitOfWork unitOfWork, IUserRepository UserRepository, IMapper mapper, IExceptionManager exceptionManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            //this.exceptionManager = exceptionManager;
            this.UserRepository = UserRepository;
            this.exceptionManager = exceptionManager;

        }

        public  OperationResult<UserDTO> Create(UserDTO item)
        {
            if(this.Find(item) == true)
            {
                Message message = new Message("", "Email Already Exists");
                return new OperationResult<UserDTO>(item, false, message);
            }

            if(item.UserEmail == "admin@movie.com")
            {
                item.Role = "Admin";
            }

            using (var hmac = new HMACSHA512())
            {
                item.PasswordKey = hmac.Key;
                item.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(item.Password));
            }
            
            UserDomain user = mapper.Map<UserDTO, UserDomain>(item);

            OperationResult result;
            //- As a normal practice just use repository and UoW to do CUD operations, else see #4 below.
            //2.1. Use repository to add domain entity in DBSet
            UserRepository.Create(user);

            //3. Save changes to database
            result =  unitOfWork.Commit();

            //- Transaction mechanism should be used if there are calls to other AppServices as well.
            //2.2. Begin transaction
            //using (var transaction = UnitOfWork.BeginTransaction())
            //{
            //    //Lets say we have to call another Appservice method here (which may have its own UoW commit).
            //    //this.Delete(item);

            //    //4.1. Use repository to add domain entity in DBSet
            //    _prodRepository.Insert(product);

            //    //4.2. Save changes to database
            //    result = UnitOfWork.Commit();

            //    //4.3. Commit transaction
            //    transaction.CommitTransaction();
            //}

            //5. Map the "Identity" field directly
            item.Id = user.Id;

            //6. Prepare the response
            return new OperationResult<UserDTO>(item, result.IsSuccess, result.MainMessage, result.AssociatedMessages.ToList<Message>());
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        public OperationResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            IEnumerable<UserDomain> productList = UserRepository.Get(x=> x.Role == "User").ToList<UserDomain>();
            List<UserDTO> UserDTOList = new List<UserDTO>();
            UserDTOList = mapper.Map<IEnumerable<UserDomain>, List<UserDTO>>(productList);
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<UserDTO>>(UserDTOList, true, message);
        }

        public bool Find(UserDTO item)
        {
            Expression<Func<UserDomain, bool>> expression = u => u.UserEmail == item.UserEmail;
            return UserRepository.Contains(expression);
        }

        public OperationResult<UserDTO> GetUserByID(int id)
        {
            Expression<Func<UserDomain, bool>> expression = u => u.Id == id;
            UserDomain user = UserRepository.Find(expression);
            if (user != null)
            {
                UserDTO item = mapper.Map<UserDomain, UserDTO>(user);
                Message message = new Message(String.Empty, "Successfully Found");
                return new OperationResult<UserDTO>(item, true, message);

            }
            else
            {
                UserDTO item = new UserDTO();
                Message message = new Message(String.Empty, "User not found");
                return new OperationResult<UserDTO>(item, false, message);
            }
        }
        public bool isMatched(string Password, byte[] PasswordHash, byte[] PasswordKey)
        {
            using (var hmac = new HMACSHA512(PasswordKey))
            { 
                var hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for(int i = 0; i < hashedPassword.Length; i++)
                {
                    if (hashedPassword[i] != PasswordHash[i]) return false;
                }
                return true;
            }
        }
        public OperationResult<UserDTO> Login(UserDTO item)
        {
            Expression<Func<UserDomain, bool>> expression = u => u.UserEmail == item.UserEmail;
            UserDomain user = UserRepository.Find(expression);
            
            if (user != null)
            {
                if(isMatched(item.Password, user.PasswordHash, user.PasswordKey))
                {
                    item.Role = user.Role;
                    item.UserName = user.UserName;
                    item.Id = user.Id;
                    Message message = new Message(string.Empty, "Login Successful");
                    return new OperationResult<UserDTO>(item, true, message);
                }
                else
                {
                    Message message = new Message(string.Empty, "Wrong password. Please enter correct password");
                    return new OperationResult<UserDTO>(item, false, message);
                }
            }
            else
            {
                Message message = new Message(string.Empty, "Email Id not registered.");
                return new OperationResult<UserDTO>(item, false, message);
            }
        }

        public OperationResult<UserDTO> DeleteUser(int id)
        {
            UserRepository.Delete(id);
            unitOfWork.Commit();
            var result = this.GetUserByID(id);
            return result;
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = UserRepository.Find(x => x.UserEmail == email);
            UserDTO userDTO = null;
            if (user != null)
            {
                userDTO = mapper.Map<UserDomain, UserDTO>(user);
            }
            return userDTO;

        }

        public async Task<OperationResult<UserDTO>> ResetPassword(ResetPasswordDto resetpassword)
        {
            var id = Int32.Parse(resetpassword.Uid);
            var user = UserRepository.GetById(id);
          
            using (var hmac = new HMACSHA512())
            {
                user.PasswordKey = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(resetpassword.Password));
            }
            var status = await unitOfWork.SaveAsyc();
            var userDto = mapper.Map<UserDomain, UserDTO>(user);
            if(status == 1)
            {
                var message = new Message("true", "Password is updated Successfully");
                return new OperationResult<UserDTO>(userDto, true, message);
            }
            else
            {
                var message = new Message("false", "Password is not updated somethiing went wrong");
                return new OperationResult<UserDTO>(userDto, false, message);
            }

        }
    }
}
