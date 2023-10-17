using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamUp.DAL.Interfaces;

namespace TeamUp.DAL.Repository
{
    public class GenericRepository<Tmodel>: IGenericRepository<Tmodel> where Tmodel : class
    {
        private readonly TeamUpContext _dbContext;

        public GenericRepository(TeamUpContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tmodel> Obtain(Expression<Func<Tmodel, bool>> filter)
        {
            try
            {
                Tmodel model = await _dbContext.Set<Tmodel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tmodel> Create(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Edit(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(Tmodel model)
        {
            try
            {
                _dbContext.Set<Tmodel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<Tmodel>> Consult(Expression<Func<Tmodel, bool>> filter = null)
        {
            try
            {
                IQueryable<Tmodel> queryModel = filter == null ? _dbContext.Set<Tmodel>() : _dbContext.Set<Tmodel>().Where(filter);
                return queryModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
