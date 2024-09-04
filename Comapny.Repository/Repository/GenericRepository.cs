using Comapny.Repository.Interfaces;
using Company.Data.Context;
using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comapny.Repository.Repository
{
    public class GenericRepository<T> : IGeneralRepository<T> where T : BaseEntity
    {
        private readonly CompanyDBContext _context;

        public GenericRepository(CompanyDBContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> GetAll()

           => _context.Set<T>().ToList();



        public T GetById(int id)

            => _context.Set<T>().FirstOrDefault(x => x.Id == id);



        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
