namespace ParkyAPI.Repository
{
    using ParkyAPI.Data;
    using ParkyAPI.Models;
    using ParkyAPI.Repository.IRepository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(a=>a.Name).ToList();
        }
        public NationalPark GetNationalPark(int id)
        {
            return _db.NationalParks.FirstOrDefault(x => x.Id == id);
        }

        public bool NationalParksExists(string name)
        {
            return _db.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParksExists(int id)
        {
            return _db.NationalParks.Any(x => x.Id==id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true:false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
