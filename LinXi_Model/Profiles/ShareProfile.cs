using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class ShareProfile : Profile
    {
        public ShareProfile()
        {
            //前者映射后者
            CreateMap<LxShare, ShareDTO>()
                .ForMember(dto => dto.ShareLink, option => option.MapFrom(e => e.LxShareLink))
                .ForMember(dto => dto.ValidCode, option => option.MapFrom(e => e.LxValidCode));
        }
    }
}