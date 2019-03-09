using System.Collections.Generic;

namespace LinksTracker2
{
    public interface IRepository<TEnt, in TPk> where TEnt : class
    {
        TEnt FindOne(TPk id);
        IEnumerable<TEnt> GetAll();
        void Update(TEnt record);
        void Delete(TEnt record);
        TEnt Create(TEnt record);
        void Dispose();
        void Save();
    }
}
