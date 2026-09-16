using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll(CancellationToken ct);
        Task<T> GetById(string id, CancellationToken ct);
    }
}
