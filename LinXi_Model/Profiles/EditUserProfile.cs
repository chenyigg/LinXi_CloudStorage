using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class EditUserProfile : Profile
    {
        public EditUserProfile()
        {
            //将Users变成DTO
            CreateMap<LxUsers, EditUserDTO>()
                .ForMember(dto => dto.Nick, option => option.MapFrom(e => e.LxUsersName))
                .ForMember(dto => dto.Email, option => option.MapFrom(e => e.LxUsersLoginEmail))
                .ForMember(dto => dto.Phone, option => option.MapFrom(e => e.LxUsersPhone));
        }
    }
}