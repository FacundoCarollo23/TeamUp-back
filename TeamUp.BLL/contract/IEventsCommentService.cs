using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.DTO;

namespace TeamUp.BLL.contract
{
    public interface IEventsCommentService
    {
        Task<List<EventsCommentDTO>> List();
        Task<EventsCommentDTO> GetById(int id);
        Task<bool> Delete(int id);
    }
}
