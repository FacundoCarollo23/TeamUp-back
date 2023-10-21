using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    public class EventsCommentDTO
    {
        public int EventCommentId { get; set; }

        public int EventId { get; set; }

        public string UserName { get; set; }    //Matchear el user_id con la tabla USERS para traer el nombre.

        public string? Comment { get; set; }

        public DateTime? DateTime { get; set; } //Hacer una lógica para restar días

    }
}
