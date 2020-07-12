using LinXi_Model;
using LinXi_IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LinXi_Repository
{
    public class LxResourceAccountRepository : BaseRepository<LxResourceAccount>, ILxResourceAccountRepository
    {
        public LxResourceAccountRepository(DbContext db) : base(db)
        {
        }
    }
}