using System.ComponentModel.DataAnnotations;

namespace Kitchen.web;

public class ShopingCart
{
    public int UserId { get; set; }

    public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();
    public ShopingCart(int userid)
    {
        UserId = userid;
    }
    public string Adress { get; set; }
    public double TotalPric
    {
        get
        {
            double totalprice = 0;
            foreach (var item in Items)
            {
                totalprice += item.Price * item.Quantity;
            }
            return totalprice;
        }
    }
}
