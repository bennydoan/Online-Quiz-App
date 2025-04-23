using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuizPage
{
    public class QuizzesModel : PageModel
    {
        private readonly AppDbContext _context;
        public QuizzesModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz newQuiz { get; set; }

        public IEnumerable<Quiz> Quizzes { get; set; }= new List<Quiz>(); // Initialize the list to avoid null reference exceptions

        public void OnGet()
        {
            Quizzes = _context.Quizzes; // Fetch all quizzes from the database, and use ToList() to convert the result to a list
        }
        public async Task<IActionResult> OnPost()

        {
            await _context.Quizzes.AddAsync(newQuiz);
            await _context.SaveChangesAsync();

            return RedirectToPage("Quizzes");

        }

    }
}
