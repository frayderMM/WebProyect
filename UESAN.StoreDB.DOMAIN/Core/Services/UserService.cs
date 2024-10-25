using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.StoreDB.DOMAIN.Core.DTO;
using UESAN.StoreDB.DOMAIN.Core.Entities;
using UESAN.StoreDB.DOMAIN.Core.Interfaces;

namespace UESAN.StoreDB.DOMAIN.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepository userRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<UserResponseAuthDTO> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email, password);
            if (user == null) return null;

            
            var token = _jwtService.GenerateJWToken(user);
            var sendEmail = false;
            var userDTO = new UserResponseAuthDTO()
            {
                Id = (int)user.Id,
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                Token = token,
                IsEmailSent = sendEmail
            };

            return userDTO;
        }

        public async Task<bool> SignUp(UserRequestAuthDTO userDTO)
        {
            var user = new User();
            user.Address = userDTO.Address;
            user.Email = userDTO.Email;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Country = userDTO.Country;
            user.DateOfBirth = userDTO.DateOfBirth;
            user.IsActive = true;
            user.Type = "U";
            return await _userRepository.SignUp(user);
        }
    }
}
