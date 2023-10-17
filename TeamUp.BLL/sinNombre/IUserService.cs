using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.DTO;

namespace TeamUp.BLL.sinNombre
{
    public interface IUserService
    {
        Task<List<UserDTO>> List();
        Task<SesionDTO> ValidateData(string email, string pass);
        Task<UserDTO> Create(UserDTO model);
        Task<bool> Edit(UserDTO model);
        Task<bool> Delete(int id);
    }
}
