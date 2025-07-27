namespace OrderManagementSystem.Models
{
    public enum Role
    {
        Admin,
        Customer
    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}