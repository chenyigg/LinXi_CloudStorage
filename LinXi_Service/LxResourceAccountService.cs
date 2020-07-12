using System;
using System.Collections.Generic;
using System.Text;
using LinXi_Model;
using LinXi_IService;
using LinXi_IRepository;

namespace LinXi_Service
{
    public class LxResourceAccountService : BaseService<LxResourceAccount>, ILxResourceAccountService
    {
        public LxResourceAccountService(ILxResourceAccountRepository LxResourceAccountDAL) : base(LxResourceAccountDAL)
        {
        }
    }
}