using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamUp.BLL.contract;
using TeamUp.DAL.Interfaces;
using TeamUp.DTO;
using TeamUp.Model;


namespace TeamUp.BLL.Service
{
    public class UserService:IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<UserDTO>> ListUser()
        {
            try
            {
                var queryUser = await _userRepository.Consult();
                var listUser = queryUser.ToList();

                return _mapper.Map<List<UserDTO>>(listUser);
            }
            catch
            {
                throw;
            }
        }

        private string GenerarToken(string UserId)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId));

            var credencialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialsToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }

        public async Task<SesionDTO> ValidateData(string email, string pass)
        {
            try
            {
                var queryUser = await _userRepository.Consult(u =>
                u.Email == email &&
                u.Password == pass);


                if (queryUser.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                User returnUser = queryUser.First();

                SesionDTO sesion = new SesionDTO();

                sesion.UserId = returnUser.UserId;
                sesion.UserName = returnUser.UserName;
                sesion.UserLastname = returnUser.UserLastname;
                sesion.Token = GenerarToken(returnUser.UserId.ToString());

                return _mapper.Map<SesionDTO>(sesion);

            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> CreateUser(UserDTO model)
        {
            try
            {
                var userCreate = await _userRepository.Create(_mapper.Map<User>(model));

                if (userCreate.UserId == 0)
                    throw new TaskCanceledException("No se pudo crear");

                var query = await _userRepository.Consult(u => u.UserId == userCreate.UserId);

                return _mapper.Map<UserDTO>(userCreate);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditUser(UserDTO model)
        {
            try
            {
                var userModel = _mapper.Map<User>(model);

                var userFound = await _userRepository.Obtain(u => u.UserId == userModel.UserId);

                if (userFound == null)
                    throw new TaskCanceledException("El usuario no Existe");

                userFound.UserName = userModel.UserName;
                userFound.UserLastname = userModel.UserLastname;
                userFound.Password = userModel.Password;

                bool res = await _userRepository.Edit(userFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo editar");

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userFound = await _userRepository.Obtain(u => u.UserId == id);

                if (userFound == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool res = await _userRepository.Delete(userFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar");

                return res;
            }
            catch
            {
                throw;
            }
        }
    }
}
