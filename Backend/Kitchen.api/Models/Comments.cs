using System.ComponentModel.DataAnnotations;

namespace Kitchen.api.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(1)]
        public int Score { get; set; }
        public string Deccription { get; set; }
        public bool Accepted { get; set; } = false;
        public int UserId { get; set; }

        //Navigations
        public virtual User User { get; set; }
    }
}
