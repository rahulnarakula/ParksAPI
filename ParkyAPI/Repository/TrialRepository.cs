namespace ParkyAPI.Repository
{
    using Microsoft.EntityFrameworkCore;

    using ParkyAPI.Data;
    using ParkyAPI.Models;
    using ParkyAPI.Repository.IRepository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TrialRepository : ITrialRepository
    {
        private readonly ApplicationDbContext _db;
        public TrialRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTrial(Trial trial)
        {
            _db.Trials.Add(trial);
            return Save();
        }

        public bool DeleteTrial(Trial trial)
        {
            _db.Trials.Remove(trial);
            return Save();
        }

        public ICollection<Trial> GetTrials()
        {
            return _db.Trials.Include(c => c.NationalPark).OrderBy(a=>a.Name).ToList();
        }
        public Trial GetTrial(int id)
        {
            return _db.Trials.Include(c => c.NationalPark).FirstOrDefault(x => x.Id == id);
        }

        public bool TrialsExists(string name)
        {
            return _db.Trials.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool TrialsExists(int id)
        {
            return _db.Trials.Any(x => x.Id==id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true:false;
        }

        public bool UpdateTrial(Trial trial)
        {
            _db.Trials.Update(trial);
            return Save();
        }

        public ICollection<Trial> GetTrialsInNationalPark(int npId)
        {
            return _db.Trials.Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();
        }
    }
}
