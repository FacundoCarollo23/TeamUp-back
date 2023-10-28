namespace TeamUp.DTO


{
    // Esta clase se utiliza para crear, editar, y eliminar los eventos personales de cada usuario.
    public class EventUserDTO
    {
        public int EventId { get; set; }

        public int ActivityId { get; set; }

        public string ActivityText { get; set; } //sacaria

        public int DifficultyLevelId { get; set; }

        public string DifficultyLevelText { get; set; } //sacaria

        public int? CountryId { get; set; }

        public string CountryText { get; set; } //sacaria

        public string? EventName { get; set; } //Editar

        public string? EventDescription { get; set; } //Editar

        public string? City { get; set; } //Editar

        public string DateTime { get; set; }    //editar solo horario; 

                
    }
}


//o agregar alguna lógica para que notifique a los articipantes si hay cambios de fecha y hora. 
//Agregar otra lógica para que se pueda editar hasta 48 hs antes. 
