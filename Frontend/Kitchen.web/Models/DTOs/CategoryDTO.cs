namespace Kitchen.web;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string svg { get; set; }
    public string Image { get; set; }
}
public class AddCategoryDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile svg { get; set; }
    public IFormFile Image { get; set; }
}
public class UpdateCategoryDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile svg { get; set; }
    public IFormFile Image { get; set; }
}