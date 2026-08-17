using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models
{
    public class UserLoginDTO
    {
        public int Id {get; set;}
        public string UserName {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;

    }
}