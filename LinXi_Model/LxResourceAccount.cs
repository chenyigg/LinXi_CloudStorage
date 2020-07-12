using System;
using System.Collections.Generic;

namespace LinXi_Model
{
    public partial class LxResourceAccount
    {
        public LxResourceAccount()
        {
            LxShare = new HashSet<LxShare>();
        }

        public int LxResourceAccountId { get; set; }
        public string LxResourceAccountName { get; set; }
        public DateTime LxResourceAccountTime { get; set; }
        public int LxResourceId { get; set; }
        public int LxUsersId { get; set; }
        public int LxPid { get; set; }
        public string LxGuid { get; set; }
        public bool LxRecycle { get; set; }

        public virtual LxResource LxResource { get; set; }
        public virtual LxUsers LxUsers { get; set; }
        public virtual ICollection<LxShare> LxShare { get; set; }
    }
}
