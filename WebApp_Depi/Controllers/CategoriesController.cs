using Microsoft.AspNetCore.Mvc;
using WebApp_Depi.core.DTOs;
using WebApp_Depi.core.Interfaces;
using WebApp_Depi.core.Modules;

namespace WebApp_Depi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet] // api/categories
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var Categories = await _categoryService.GetAllAsync();
                if (Categories is null || !Categories.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "no Categories Found!",
                        Data = new List<Category>()
                    });
                }
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Categoriries retrived successfully",
                    Data = Categories
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while retriving data.",
                    Error = ex.Message
                });
            }

        }

        [HttpGet("{id}")]  // api/categories/3
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Category = await _categoryService.GetByIdAsync(id);
                if (Category is null)
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Category With ID {id} Not Found",
                    });
                }
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Category Retrived Successfully",
                    Data = Category
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while retriving data.",
                    Error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryDTo categoryDTo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Message = "Invalid Category Data!",
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                var Category = await _categoryService.CreateAsync(categoryDTo);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "Category Created Successfully",
                    Data = Category,
                    StatusCode = StatusCodes.Status201Created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while Creating data.",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CategoryDTo categoryDTo)
        {
            try
            {
                var OldCategory = await _categoryService.GetByIdAsync(id);
                if (OldCategory is null)
                    return BadRequest(new
                    {
                        Message = $"Category With ID {id} Not Found",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                if (await _categoryService.UpdateAsync(id, categoryDTo))
                    return Ok(new
                    {
                        Message = "Category Updated Successfully",
                        Data = await _categoryService.GetByIdAsync(id),
                        StatusCode = StatusCodes.Status200OK
                    });
                else
                    return NotFound(new
                    {
                        Message = "Category Npt Updated",
                        StatusCode = StatusCodes.Status404NotFound
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while Updating data.",
                    Error = ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> Edit(CategoryDTo categoryDTo)
        {
            try
            {
                var OldCategory = await _categoryService.GetByIdAsync(categoryDTo.Id);
                if (OldCategory is null)
                    return BadRequest(new
                    {
                        Message = $"Category With ID {categoryDTo.Id} Not Found",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                if (await _categoryService.UpdateAsync(categoryDTo))
                    return Ok(new
                    {
                        Message = "Category Updated Successfully",
                        Data = await _categoryService.GetByIdAsync(categoryDTo.Id),
                        StatusCode = StatusCodes.Status200OK
                    });
                else
                    return NotFound(new
                    {
                        Message = "Category Npt Updated",
                        StatusCode = StatusCodes.Status404NotFound
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while Updating data.",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Category = await _categoryService.GetByIdAsync(id);
                if (Category is null)
                    return NotFound(new
                    {
                        Message = "Category Not Found",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                if (await _categoryService.DeleteAsync(id))
                    return Ok(new
                    {
                        Message = "Category Deleted Successfully",
                        OldData = Category,
                        StatusCode = StatusCodes.Status200OK
                    });
                else
                    return BadRequest(new
                    {
                        Message = "Category Not Deleted ",
                        Data = await _categoryService.GetByIdAsync(id),
                        StatusCode = StatusCodes.Status400BadRequest
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An Error Occured while Deleting data.",
                    Error = ex.Message
                });
            }
        }
    }
}
