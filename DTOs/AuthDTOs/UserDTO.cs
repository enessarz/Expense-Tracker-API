using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class UserDTO
    {
        public int Id {get; set;}
        public string UserName {get; set;} = string.Empty;

        public List<ExpenseModel> Expenses {get; set;} = new List<ExpenseModel>(); 
    }
}