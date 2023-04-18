namespace AlumniNetAPI.DTOs
{
    public class UserDTO
    {
        public string UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsValid { get; set; }

        public string? Email { get; set; }
    }
}
