namespace AlumniNetAPI.DTOs
{
    public class EventInviteDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string Image { get; set; }
        public int InviteId { get; set; }
        public string Status { get; set; }
    }
}
