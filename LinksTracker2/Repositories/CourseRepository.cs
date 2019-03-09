using LinksTracker2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LinksTracker2.Repositories
{
    public class CourseRepository : IRepository<Course, int>
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository()
        {
            _db = new ApplicationDbContext();
        }

        public Course Create(Course record)
        {
            _db.Courses.Add(record);
            Save();
            return record;
        }

        public void Delete(Course record)
        {
            _db.Courses.Remove(record);
            Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Course FindOne(int id)
        {
            return _db.Courses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Course> GetAll()
        {
            return _db.Courses.ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Course record)
        {
            _db.Entry(record).State = EntityState.Modified;
            Save();
        }

        public List<SelectListItem> GetDropdown()
        {
            return _db.Courses.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                              .Distinct()
                              .ToList();
        }
    }
}