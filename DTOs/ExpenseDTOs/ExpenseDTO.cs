using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class ExpenseDTO
    {
        public int Id {get; set;}
        public string Category {get; set;} = string.Empty;
        public int Amount {get; set;}
        public DateTime Date {get; set;} = DateTime.UtcNow;

        public int UserId {get; set;}

    }
}