using System;
using System.Collections.Generic;

namespace LinXi_Model
{
    public partial class LxShare
    {
        public int LxShareId { get; set; }
        public int LxUserId { get; set; }
        public int LxResourceAccountId { get; set; }
        public string LxShareLink { get; set; }
        public string LxValidCode { get; set; }
        public string LxCodeKey { get; set; }
        public string LxShareTime { get; set; }
        public DateTime LxCreateTime { get; set; }

        public virtual LxResourceAccount LxResourceAccount { get; set; }
        public virtual LxUsers LxUser { get; set; }
    }
}
