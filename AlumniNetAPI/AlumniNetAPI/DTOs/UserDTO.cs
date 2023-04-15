namespace AlumniNetAPI.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool IsValid { get; set; }

        public string? Email { get; set; }

        public string? FirebaseAuthToken { get; set; }
    }
}
