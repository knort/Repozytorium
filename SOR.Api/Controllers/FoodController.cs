using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SOR.BLL;
using SOR.Model;
using SOR.Model.ViewModels;
using SOR.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SOR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.Constants.ADMIN_POLICY)]
    public class FoodController : SORBaseController<Food,FoodViewModel>
    {
        private readonly IBaseService<Food> _foodService;
        private readonly IMapper _mapper;

        public FoodController(IBaseService<Food> foodService, IMapper mapper) : base(foodService, mapper)
        {
            _foodService = foodService;
            _mapper = mapper;
        }
        

        [HttpGet("foodForMenu/{menuId}")]
        public async Task<IActionResult> GetFoodForMenu(Guid menuId)
        {
            var allFoods = await _foodService.GetAllAsync();
            var menuFoods = await _foodService.GetAllCustomAsync(food => food.MenuFoods.Any(menufood => menufood.MenuId == menuId));
            var foodAllowed = allFoods.Except(menuFoods);
            if (foodAllowed != null)
            {
                var foodAllowedViewModel = _mapper.Map<ICollection<FoodViewModel>>(foodAllowed);
                return Ok(foodAllowed);
            }
            return NoContent();
        }
    }
}