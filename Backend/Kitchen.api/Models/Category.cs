namespace Kitchen.api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string svg { get; set; }
        public string Image { get; set; }
        public bool Deleted { get; set; } = false;

        //Navigations
        public virtual ICollection<CategoryFood> CategoryFoods { get; set; }
    }
}