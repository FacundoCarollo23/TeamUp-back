using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.DTO;

namespace TeamUp.BLL.contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> ListUser();
        Task<SesionDTO> ValidateData(string email, string pass);
        Task<UserDTO> CreateUser(UserDTO model);
        Task<bool> EditUser(UserDTO model);
        Task<bool> DeleteUser(int id);
    }
}
