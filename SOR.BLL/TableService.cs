using SOR.Model;
using SOR.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.BLL
{
    public class TableService : BaseService<Table>, ITableService
    {
        private readonly IRepository<Table> _repository;

        public TableService(IRepository<Table> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Table>> GetFreeTablesList()
        {
            var today = DateTime.Now.Date;
            return await _repository.GetAllCustomsAsync(table => table.Reservation.Count == 0 ^
            table.Reservation.Any(res => today != res.End.Date || (today == res.End.Date && res.End < DateTime.Now)));
        }
    }
}
