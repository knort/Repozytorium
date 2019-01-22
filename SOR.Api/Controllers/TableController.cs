using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOR.BLL;
using SOR.Model;
using SOR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.Constants.USER_POLICY)]
    public class TableController : SORBaseController<Table, TableViewModel>
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TableController(ITableService tableService, IMapper mapper) : base(tableService, mapper)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        [HttpGet("freeTables")]
        public async Task<IActionResult> GetNotReservedTables()
        {
            var freeTables = await _tableService.GetFreeTablesList();
            var tablesViewModels = _mapper.Map<ICollection<TableViewModel>>(freeTables);
            return Ok(tablesViewModels);
        }

        [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
        public override async Task<IActionResult> PostAsync([FromBody] TableViewModel viewmodel)
        {
            return await base.PostAsync(viewmodel);
        }

        [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
        public override async Task<IActionResult> UpdateAsync([FromBody] TableViewModel viewmodel)
        {
            return await base.UpdateAsync(viewmodel);
        }
    }
}
