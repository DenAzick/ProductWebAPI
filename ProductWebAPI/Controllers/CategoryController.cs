using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Context;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]


public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Category>> GetList()
    {
        return await _context.Categories.ToListAsync();
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto.ParentId != null 
            && !await _context.Categories
            .AnyAsync(c => c.Id == createCategoryDto.ParentId))
        {
            return NotFound();
        }

        var category = new Category()
        {
            Name = createCategoryDto.Name,
            ParentId = createCategoryDto.ParentId,
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }


    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateCategoryDto createCategoryDto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        category.Name = createCategoryDto.Name;
        category.ParentId = createCategoryDto.ParentId;

        await _context.SaveChangesAsync();

        return Ok(category);
    }
}
