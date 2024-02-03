namespace Kitchen.api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int FoodId { get; set; }
        public string FoodTitle { get; set; }
        public double Price { get; set; }

        //Navigations
        public virtual Order Order { get; set; }
    }
}