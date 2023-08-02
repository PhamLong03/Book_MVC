using BookWeb.Repository.IReporistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IReporistory
{
    public interface IUnit
    {
        ICategoryRepository UnitCategory{ get; }
        IProductRepository UnitProduct{ get; }
        void Save();
    }
}
