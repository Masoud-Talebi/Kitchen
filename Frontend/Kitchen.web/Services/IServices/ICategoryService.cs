namespace Kitchen.web;

public interface ICategoryService
{
    Task<T> GetAllCategory<T>();
    Task<T> GetCategorybyID<T>(int Id);
    Task<T> AddCategory<T>(AddCategoryDTO addCategory, string AccessToken);
    Task<T> UpdateCategory<T>(UpdateCategoryDTO updateCategory, string AccessToken);
    Task<T> RemoveCategory<T>(int Id, string AccessToken);
}
