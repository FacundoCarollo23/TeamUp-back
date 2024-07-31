using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DAL.Interfaces
{
    public interface IGenericRepository<Tmodel> where Tmodel : class
    {
        Task<Tmodel> Obtain(Expression<Func<Tmodel, bool>> filter);

        Task<Tmodel> Create(Tmodel model);

        Task<bool> Edit(Tmodel model);

        Task<bool> Delete(Tmodel model);

        Task<IQueryable<Tmodel>> Consult(Expression<Func<Tmodel, bool>> filter = null);
    }
}
