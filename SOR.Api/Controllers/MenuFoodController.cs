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

    [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
    public class MenuFoodController : SORBaseController<MenuFood, MenuFoodViewModel>
    {
        public MenuFoodController(IBaseService<MenuFood> baseService, IMapper mapper) : base(baseService, mapper)
        {

        }

        [Authorize(Policy = Constants.Constants.USER_POLICY)]
        public async override Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        [Authorize(Policy = Constants.Constants.USER_POLICY)]
        public override Task<IActionResult> GetByIdAsync(Guid id)
        {
            return base.GetByIdAsync(id);
        }
    }
}
