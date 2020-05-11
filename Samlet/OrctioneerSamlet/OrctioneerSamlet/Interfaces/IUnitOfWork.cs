using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VareDatabase.Interfaces.Auction;
using VareDatabase.DBContext;

namespace VareDatabase.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        VareDataModelContext Context { get;}
        int Commit();
    }
}
