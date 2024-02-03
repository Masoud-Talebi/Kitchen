using System.ComponentModel.DataAnnotations;

namespace Kitchen.web;

public class CommentDTO
{
    public int Id { get; set; }
    [MaxLength(1)]
    public int Score { get; set; }
    public string Deccription { get; set; }
    public bool Accepted { get; set; } = false;
    public int UserId { get; set; }
    public UserDTO User { get; set; }
}
public class AddCommentDTO
{
    public int Score { get; set; }
    public string Deccription { get; set; }
    public int? UserId { get; set; }

}
