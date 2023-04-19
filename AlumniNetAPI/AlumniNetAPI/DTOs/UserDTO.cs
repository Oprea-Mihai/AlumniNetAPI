namespace AlumniNetAPI.DTOs
{
    public class UserDTO
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsValid { get; set; }

        public string? Email { get; set; }

        public int ProfileId { get; set; }
    }
}
