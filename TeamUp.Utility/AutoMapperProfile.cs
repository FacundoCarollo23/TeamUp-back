using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.Utility
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<User, UserDTO>()
                .ForMember(des =>
                des.DateOfBirthText,
                opt => opt.MapFrom(origin => origin.DateOfBirth.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<UserDTO, User>()
                .ForMember(des =>
                des.DateOfBirth,
                opt => opt.MapFrom(origin => DateTime.ParseExact(origin.DateOfBirthText, "dd/MM/yyyy", new CultureInfo("es-PE")))
                );

           CreateMap<User, SesionDTO>()
                .ReverseMap();
            #endregion User

            #region EventUser
            CreateMap<Event, EventUserDTO>()
               .ForMember(des =>
               des.DateTime,
               opt => opt.MapFrom(origin => origin.DateTime.ToString("dd/MM/yyyy H:mm"))
               );

            CreateMap<EventUserDTO, Event>()
                .ForMember(des =>
                des.DateTime,
                opt => opt.MapFrom(origin => DateTime.ParseExact(origin.DateTime, "dd/MM/yyyy", new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.UsersEvents,
                opt => opt.Ignore()
                );

            CreateMap<UsersEvent, EventUserDTO>()
                .ForMember(des =>
                des.UserId,
                opt => opt.MapFrom(origin => origin.UserId)
                );

            #endregion EventUser

            #region Event
            CreateMap<Event, EventDTO>()
               .ForMember(des =>
               des.ActivityName,
               opt => opt.MapFrom(origen => origen.Activity.ActivityName))
               .ForMember(des =>
               des.DifficultyName,
               opt => opt.MapFrom(origen => origen.DifficultyLevel.DifficultyName))
               .ForMember(des =>
               des.CountryName,
               opt => opt.MapFrom(origen => origen.Country.CountryName))
               .ForMember(des =>
               des.DateTime,
               opt => opt.MapFrom(origin => origin.DateTime.ToString("dd/MM/yyyy H:mm"))
               );

            CreateMap<EventDTO, Event>()
                .ForMember(destino =>
                destino.Activity,
                opt => opt.Ignore())
                .ForMember(destino =>
                destino.DifficultyLevel,
                opt => opt.Ignore())
                .ForMember(destino =>
                destino.Country,
                opt => opt.Ignore()
                );
            #endregion Event

            #region EventComment
            CreateMap<EventsComment, EventsCommentDTO>()
                .ForMember(des =>
               des.EventName,
               opt => opt.MapFrom(origen => origen.Event.EventName))
               .ForMember(des =>
               des.UserIdName,
               opt => opt.MapFrom(origen => origen.User.UserName))
                .ForMember(des =>
               des.DateTime,
               opt => opt.MapFrom(origin => origin.DateTime.Value.ToString("dd/MM/yyyy H:mm"))
               );

            CreateMap<EventsCommentDTO, EventsComment>()
                .ForMember(destino =>
                destino.Event,
                opt => opt.Ignore())
                .ForMember(destino =>
                destino.User,
                opt => opt.Ignore()
                );
            #endregion EventComment

        }
    }
}
