namespace Kitchen.api.Application.DTOs
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Score { get; set; }
        public List<CategoryDTO> Categorys { get; set; }
    }
    public class AddFoodDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public double Price { get; set; }
        public int Score { get; set; }
        public List<int> CategoryIds { get; set; }
    }
    public class UpdateFoodDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public double Price { get; set; }
        public int Score { get; set; }
    }
}