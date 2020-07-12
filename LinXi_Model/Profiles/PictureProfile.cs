using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            //前者映射后者
            CreateMap<LxResourceAccount, PictureDTO>()
                .ForMember(dto => dto.PicId, option => option.MapFrom(e => e.LxResourceAccountId))
                .ForMember(dto => dto.PicName, option => option.MapFrom(e => e.LxResourceAccountName))
                .ForMember(dto => dto.PicTime, option => option.MapFrom(e => e.LxResourceAccountTime.Date));
        }
    }
}