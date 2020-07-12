using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class ShareFileProfile : Profile
    {
        public ShareFileProfile()
        {
            //前者映射后者
            CreateMap<LxShare, ShareFileDTO>()
                .ForMember(dto => dto.LxUsersID, option => option.MapFrom(e => e.LxUserId))
                .ForMember(dto => dto.ValidCode, option => option.MapFrom(e => e.LxValidCode))
                .ForMember(dto => dto.LxShareID, option => option.MapFrom(e => e.LxShareId));
        }
    }
}