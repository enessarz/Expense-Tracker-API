using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class User
    {
        public int Id {get; set;}
        public string UserName {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;

        public List<ExpenseModel> Expenses {get; set;} = new List<ExpenseModel>(); 
    }
}