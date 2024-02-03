namespace Kitchen.api.Models
{
    public class Food
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Score { get; set; }

        //Navigations
        public virtual ICollection<CategoryFood> CategoryFoods { get; set; }
        
    }
}