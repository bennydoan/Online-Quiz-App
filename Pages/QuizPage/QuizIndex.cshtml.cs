using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuizPage
{
    public class QuizIndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public QuizIndexModel(AppDbContext context)
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
