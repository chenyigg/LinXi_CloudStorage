using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class AllFileProfile : Profile
    {
        public AllFileProfile()
        {
            //前者映射后者
            CreateMap<LxResourceAccount, AllFileDTO>()
                .ForMember(dto => dto.lxPid, option => option.MapFrom(entity => entity.LxPid));
        }
    }
}