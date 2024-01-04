using ToDoListAPI.Enums;

namespace ToDoListAPI.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public Status Status { get; set; }

        public DateTime Deadline { get; set; }
    }
}
