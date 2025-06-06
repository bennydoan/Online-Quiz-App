using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;
using Quiz_Application2.Pages.QuizPage;

namespace Quiz_Application2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context) // Dependcy injection
        {
            _context = context;
        }

        public IEnumerable<Quiz> Quizzes { get; set; } = new List<Quiz>(); // Initialize the list to avoid null reference exceptions


        public void OnGet()
        {
            Quizzes = _context.Quizzes.ToList(); // Fetch all quizzes from the database
        }
    }
}
