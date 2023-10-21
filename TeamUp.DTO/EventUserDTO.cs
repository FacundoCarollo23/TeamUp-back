using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeamUp.DTO
{
    public class EventUserDTO
    {
        public int EventId { get; set; }

        public string ActivityText { get; set; }

        public string DifficultyLevelText { get; set; }

        public string CountryText { get; set; }

        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

        public string? City { get; set; }

        public string DateTime { get; set; }

                
    }
}
