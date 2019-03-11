using LinksTracker2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LinksTracker2.Repositories
{
    public class StatsRepository : IRepository<Stats, int>
    {
        private readonly ApplicationDbContext _db;

        public StatsRepository()
        {
            _db = new ApplicationDbContext();
        }

        public Stats Create(Stats record)
        {
            _db.Stats.Add(record);
            Save();

            return record;
        }

        public void Delete(Stats record)
        {
            _db.Stats.Remove(record);
            Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Stats FindOne(int id)
        {
            return _db.Stats.Include(c => c.Hole).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Stats> GetAll()
        {
            return _db.Stats.ToList();
        }

        public List<Stats> GetAllByUserId(string id)
        {
            return _db.Stats.Where(s => s.UserId == id).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Stats record)
        {
            _db.Entry(record).State = EntityState.Modified;
            Save();
        }
    }
}