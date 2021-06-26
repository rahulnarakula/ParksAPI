namespace ParkyAPI.Repository.IRepository
{
    using ParkyAPI.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITrialRepository
    {
        ICollection<Trial> GetTrials();
        ICollection<Trial> GetTrialsInNationalPark(int npId);
        Trial GetTrial(int id);
        bool TrialsExists(string name);
        bool TrialsExists(int id);
        bool CreateTrial(Trial trial);
        bool UpdateTrial(Trial trial);
        bool DeleteTrial(Trial trial);
        bool Save();

    }
}
