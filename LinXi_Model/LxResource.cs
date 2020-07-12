using System;
using System.Collections.Generic;

namespace LinXi_Model
{
    public partial class LxResource
    {
        public LxResource()
        {
            LxResourceAccount = new HashSet<LxResourceAccount>();
        }

        public int LxResourceId { get; set; }
        public string LxResourceName { get; set; }
        public string LxResourceSize { get; set; }
        public int LxResourceCategoryId { get; set; }
        public string LxResourceMdfive { get; set; }
        public string LxResourcePath { get; set; }

        public virtual ICollection<LxResourceAccount> LxResourceAccount { get; set; }
    }
}
