using System;
using System.Collections.Generic;

namespace LinXi_Model
{
    public partial class LxUsers
    {
        public LxUsers()
        {
            LxResourceAccount = new HashSet<LxResourceAccount>();
            LxShare = new HashSet<LxShare>();
        }

        public int LxUsersId { get; set; }
        public string LxUsersName { get; set; }
        public string LxUsersPhone { get; set; }
        public string LxUsersLoginName { get; set; }
        public string LxUsersLoginPwd { get; set; }
        public string LxUsersLoginEmail { get; set; }
        public string LxUsersRemark { get; set; }
        public string LxUsersPic { get; set; }

        public virtual ICollection<LxResourceAccount> LxResourceAccount { get; set; }
        public virtual ICollection<LxShare> LxShare { get; set; }
    }
}
