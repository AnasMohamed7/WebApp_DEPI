using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp_Depi.core.DTOs;
using WebApp_Depi.core.Interfaces;
using WebApp_Depi.core.Modules;
using WebApp_Depi.infrastructure.Data;

namespace WebApp_Depi.infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly WebAppDbContext _context;
        public CategoryService(WebAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var Categories = await _context.Categories.ToListAsync();

            return Categories;
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);

        }
        public async Task<Category> CreateAsync(CategoryDTo category)
        {
            var Category = new Category
            {
                Name = category.Name,
            };
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Category;
        }
        public async Task<bool> UpdateAsync(int id, CategoryDTo category)
        {
            var OldCateory = await GetByIdAsync(id);
            if (OldCateory is null)
                return false;
            OldCateory.Name = category.Name;
            _context.Categories.Update(OldCateory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(CategoryDTo category)
        {
            var OldCategory = await GetByIdAsync(category.Id);
            if (OldCategory is null)
                return false;
            OldCategory.Name = category.Name;
            _context.Categories.Update(OldCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var Category = await GetByIdAsync(id);
            if (Category is null)
                return false;
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
