using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    //Para contar la cantidad de participantes
    public class UsersContadorDTO
    {
        public int UserEventId { get; set; }

        public int RolId { get; set; }

        public string rolName { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }
    }
}
