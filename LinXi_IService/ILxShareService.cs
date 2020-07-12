using LinXi_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_IService
{
    public interface ILxShareService : IBaseService<LxShare>
    {
        LxShare CreateShare(int UserID, int LxResourceAccountID, string LxShareTime);
    }
}