namespace AlumniNetAPI.DTOs
{
    public class PostWithUserDataDTO
    {
        public int PostId { get; set; }

        public string? Image { get; set; }

        public string? Text { get; set; }

        public string? Title { get; set; }

        public DateTime PostingDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
