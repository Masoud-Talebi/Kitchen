namespace Kitchen.api.Models
{
    public class CategoryFood
    {
        public int CategoryId { get; set; }
        public int FoodId { get; set; }

        //Navigations
        public virtual Category Category { get; set; }
        public virtual Food Food { get; set; }
    }
}