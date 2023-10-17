using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.BLL.sinNombre;
using TeamUp.DAL.Interfaces;
using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.BLL.Service
{
    public class UserService:IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> List()
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

                return _mapper.Map<SesionDTO>(returnUser);

            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO model)
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

        public async Task<bool> Edit(UserDTO model)
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

        public async Task<bool> Delete(int id)
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
