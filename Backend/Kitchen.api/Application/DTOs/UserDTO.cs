using System.ComponentModel.DataAnnotations;

namespace Kitchen.api.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public bool deleted { get; set; }
        public string Role { get; set; } = "User";
    }
    public class AddUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
    }
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserDTO
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}