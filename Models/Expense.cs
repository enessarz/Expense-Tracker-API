using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class ExpenseModel
    {
        public int Id {get; set;}
        public string Category {get; set;} = string.Empty;
        public int Amount {get; set;}
        public DateTime Date {get; set;} = DateTime.UtcNow;

        public int UserId {get; set;}
        public User? User;
    }
}