using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class ShareUsersProfile : Profile
    {
        public ShareUsersProfile()
        {
            //前者映射后者
            CreateMap<LxUsers, ShareUsersDTO>()
                    .ForMember(dto => dto.ShareNick, option => option.MapFrom(e => e.LxUsersLoginName));
            //.ForMember(dto => dto.ValidCode, option => option.MapFrom(e => e.LxValidCode));
        }
    }
}