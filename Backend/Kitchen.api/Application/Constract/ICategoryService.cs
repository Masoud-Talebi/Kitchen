
using Kitchen.api.Application.DTOs;

namespace Kitchen.api.Application.Constract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategorybyID(int Id);
        Task<CategoryDTO> AddCategory(AddCategoryDTO addCategory);
        Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO updateCategory);
        Task<bool> RemoveCategory(int Id);
    }
}