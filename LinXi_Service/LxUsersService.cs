using LinXi_IRepository;
using LinXi_IService;
using LinXi_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Service
{
    public class LxUsersService : BaseService<LxUsers>, ILxUsersService
    {
        public LxUsersService(ILxUsersRepository lxUsersDAL) : base(lxUsersDAL)
        {
        }
    }
}