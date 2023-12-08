using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    // Esta clase se utiliza para comentar y puntuar eventos.
    public class EventsCommentDTO
    {
        public int EventCommentId { get; set; }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public int UserId { get; set; }

        public string UserIdName { get; set; }

        public string? UserAlias { get; set; }

        public string Comment { get; set; }

        public string DateTime { get; set; }

    }
}
