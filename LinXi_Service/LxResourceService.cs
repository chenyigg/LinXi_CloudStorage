using System;
using System.Collections.Generic;
using System.Text;
using LinXi_IRepository;
using LinXi_IService;
using LinXi_Model;

namespace LinXi_Service
{
    public class LxResourceService : BaseService<LxResource>, ILxResourceService
    {
        public LxResourceService(ILxResourceRepository LxResourceDAL) : base(LxResourceDAL)
        {
        }
    }
}