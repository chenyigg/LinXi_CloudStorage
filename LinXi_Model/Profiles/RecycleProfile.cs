using AutoMapper;
using LinXi_Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.Profiles
{
    public class RecycleProfile : Profile
    {
        public RecycleProfile()
        {
            //前者映射后者
            CreateMap<LxResourceAccount, RecycleDTO>();
        }
    }
}