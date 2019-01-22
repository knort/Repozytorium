using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SOR.BLL;
using System.Threading.Tasks;
using SOR.Constants;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace SOR.Api.Controllers
{
    [EnableCors(Constants.Constants.CORS_POLICY)]
    [ApiController]
    public abstract class SORBaseController<T, ViewModel> : ControllerBase where T : class where ViewModel: class
    {
        private readonly IBaseService<T> _baseService;
        private readonly IMapper _mapper;

        public SORBaseController(IBaseService<T> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var items = await _baseService.GetAllAsync();
            var itemViewModels = _mapper.Map<ICollection<ViewModel>>(items);
            return Ok(itemViewModels);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = await _baseService.GetByIdAsync(id);
            if (item != null)
            {
                var itemViewModel = _mapper.Map<ViewModel>(item);
                return Ok(itemViewModel);
            }
            return NotFound(new { error = "Nie znaleziono takiego id: " + id + "w bazie" });
        }

         [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody]ViewModel viewmodel)
        {
            var t = _mapper.Map<T>(viewmodel);
            bool result = await _baseService.CreateAsync(t);
            if (result)
                return Created("api/[controller]/" , t);
            return BadRequest(new { error = "Błąd podczas dodawania do bazy danych." });
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync([FromBody]ViewModel viewmodel)
        {
            T t = _mapper.Map<T>(viewmodel);
            bool result = await _baseService.UpdateAsync(t);
            if (result)
                return Ok(new { message = "Zmodyfikowano", item = t });
            return BadRequest(new { error = "Błąd podczas modyfikacji w bazie danych" });
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var food = await _baseService.GetByIdAsync(id);
            if (food != null)
            {
                bool result = await _baseService.DeleteAsync(id);
                if (result)
                    return Ok(new { message = "Usunięto id: " + id });
                return BadRequest(new { error = "Nie udało się usunąć id: " + id });
            }
            return NotFound("Nie znaleziono id: " + id);
        }

    }
}
