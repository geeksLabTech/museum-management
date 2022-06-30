using Datalayer.UnitOfWork;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;


    public class MuseumsRepository : Repository<Museum>, IMuseumRepository
    {
        

        public MuseumsRepository(DbContext context) : base(context)
        {
            
        }

        public int GetMuseumIdByName(string name)
        {
            return this.Find(x => x.Name == name).FirstOrDefault().Id;
        }
    }
