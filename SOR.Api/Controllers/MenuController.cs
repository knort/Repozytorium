using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOR.BLL;
using SOR.Model;

namespace SOR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
    public class MenuController : SORBaseController<Menu, Menu>
    {
        public MenuController(IBaseService<Menu> baseService, IMapper mapper) : base(baseService, mapper)
        {

        }

        [Authorize(Policy = Constants.Constants.USER_POLICY)]
        public async override Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        [Authorize(Policy = Constants.Constants.USER_POLICY)]
        public async override Task<IActionResult> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}