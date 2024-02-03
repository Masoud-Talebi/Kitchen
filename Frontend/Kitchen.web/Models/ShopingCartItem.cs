namespace Kitchen.web;

public class ShopingCartItem
{
    public int FoodId { get; set; }
    public string FoodTitle { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    //
    public string Image { get; set; }
    public string Description { get; set; }
}
