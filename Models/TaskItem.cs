using System;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }    // 1=高,2=中,3=低
        public DateTime DueDate { get; set; }
        public string Status { get; set; }   // "Todo", "InProgress", "Done"
    }
}