using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Depi.core.DTOs;
using WebApp_Depi.core.Modules;

namespace WebApp_Depi.core.Interfaces
{
    public interface ICategoryService
    {
        // get all
        Task<IEnumerable<Category>> GetAllAsync();
        // get by id
        Task<Category> GetByIdAsync(int id);
        // create
        Task<Category> CreateAsync(CategoryDTo category); 
        // update
        Task<bool> UpdateAsync(int id, CategoryDTo category);
        Task<bool> UpdateAsync(CategoryDTo category);
        // delete
        Task<bool> DeleteAsync(int id);
    }
}
