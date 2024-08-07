﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    // Esta clase se utiliza para mostrar eventos y datos de un evento.
    public class EventDTO
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public string? Alias { get; set; }

        public int ActivityId { get; set; }

        public string? ActivityName { get; set; }
        
        public int DifficultyLevelId { get; set; }

        public string? DifficultyName { get; set; }

        public int? CountryId { get; set; }

        public string? CountryName { get; set; }    //recordar agregar la FK en EVENT

        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

        public string? City { get; set; }

        public string? DateTime { get; set; }

        public string? CreateTime { get; set; }

        public int? UserCount { get; set; }

        //public virtual ICollection<EventsCommentDTO> EventsComments { get; set; } //Ver si necesitamos hacer Mapper.

        //public virtual ICollection<UsersContadorDTO> UsersContador { get; set; } Para contar la cantidad de participantes


    }
}
