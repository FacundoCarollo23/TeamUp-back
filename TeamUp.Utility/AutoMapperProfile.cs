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


        }
    }
}
