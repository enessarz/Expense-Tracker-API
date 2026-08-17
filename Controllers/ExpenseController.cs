using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserIdFromToken()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdString, out int userId);
            return userId;
        }

        [HttpPost("new-expense")]
        public async Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] ExpenseDTO newExpenseDto)
        {
            int currentUserId = GetUserIdFromToken();
            if (currentUserId == 0) return Unauthorized();

            var expenseEntity = new ExpenseModel
            {
                Amount = newExpenseDto.Amount,
                Category = newExpenseDto.Category,
                Date = newExpenseDto.Date,
                UserId = currentUserId
            };

            _context.Expense.Add(expenseEntity);
            await _context.SaveChangesAsync();

            newExpenseDto.Id = expenseEntity.Id;

            return CreatedAtAction(nameof(GetMyExpenses), new { id = newExpenseDto.Id }, newExpenseDto);
        }
        
        [HttpGet("my-expenses")]
        public async Task<ActionResult<UserDTO>> GetMyExpenses()
        {
            int currentUserId = GetUserIdFromToken();
            if (currentUserId == 0) return Unauthorized();

            var requestedUser = await _context.User
                                            .Include(u => u.Expenses)
                                            .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (requestedUser == null)
            {
                return NotFound("User not found.");
            }

            var userDto = new UserDTO
            {
                Id = requestedUser.Id,
                UserName = requestedUser.UserName,
                Expenses = requestedUser.Expenses 
            };

            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id) 
        {
            int currentUserId = GetUserIdFromToken();
            if (currentUserId == 0) return Unauthorized();

            var expenseToDelete = await _context.Expense.FirstOrDefaultAsync(e => e.Id == id);

            if (expenseToDelete == null) {
                return NotFound("Expense does not exist.");
            }

            if (expenseToDelete.UserId != currentUserId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You are not authorized to delete this expense.");
            }

            _context.Expense.Remove(expenseToDelete);
            await _context.SaveChangesAsync();

            return Ok("Expense deleted successfully.");
        }
    }
}