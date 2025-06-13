using System.Collections.Generic;
using SepatuApp.Models;
using SepatuApp.Repositories;

namespace SepatuApp.Controllers
{
    public class SepatuController
    {
        private readonly SepatuRepository repository = new SepatuRepository();

        public List<Sepatu> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(Sepatu sepatu)
        {
            repository.Add(sepatu);
        }

        public void Update(string oldMerk, Sepatu sepatu)
        {
            repository.Update(oldMerk, sepatu);
        }

        public void Delete(string merk)
        {
            repository.Delete(merk);
        }
    }
}
