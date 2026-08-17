using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class UserRegisterDTO
    {
        public int Id {get; set;}
        public string UserName {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;

        public List<ExpenseModel> Expenses {get; set;} = new List<ExpenseModel>(); 
    }
}