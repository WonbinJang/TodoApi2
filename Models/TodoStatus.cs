namespace TodoApi.Models
{
    public class TodoStatus
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int? ownerId {get; set;}
    }
}