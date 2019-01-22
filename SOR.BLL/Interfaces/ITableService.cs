using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOR.Model;

namespace SOR.BLL
{
    public interface ITableService : IBaseService<Table>
    {
        Task<ICollection<Table>> GetFreeTablesList();
    }
}