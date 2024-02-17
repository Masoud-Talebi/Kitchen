using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kitchen.api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public bool deleted { get; set; } = false;
        public string Role { get; set; } = "User";
        public string? PushNotifacation { get; set; }

        //Navigations
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}