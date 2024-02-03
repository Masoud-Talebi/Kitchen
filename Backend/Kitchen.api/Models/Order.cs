using System.Runtime.InteropServices;

namespace Kitchen.api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Adress { get; set; }
        public double TotalPrice { get; set; }
        public StatePay StatePay { get; set; }
        public OrderStatus  OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; } = false;
        //Navigations
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    public enum StatePay
    {
        IsPay = 1,
        NotPay = 2,
    }
    public enum OrderStatus
    {
        Complited = 1,
        Pending = 2,
        Failed = 3,
        Sending = 4,
    }
}