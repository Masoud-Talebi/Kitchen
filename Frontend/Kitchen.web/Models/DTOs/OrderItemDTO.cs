namespace Kitchen.web;

public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string FoodTitle { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    public class AddOrderItemDTO
    {
        public int FoodId { get; set; }
        public string FoodTitle { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
    public class UpdateOrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int FoodId { get; set; }
        public string FoodTitle { get; set; }
        public double Price { get; set; }
    }
