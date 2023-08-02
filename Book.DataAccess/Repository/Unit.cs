using Book.DataAccess.Data;
using Book.DataAccess.Repository.IReporistory;
using BookWeb.Repository.IReporistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class Unit : IUnit
    {
        private ApplicationDbContext _db;
        public ICategoryRepository UnitCategory { get; private set; }
        public IProductRepository UnitProduct { get; private set; }
        public Unit(ApplicationDbContext db)
        {
            _db = db;
            UnitCategory = new CategoryRepository(_db);
            UnitProduct = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
