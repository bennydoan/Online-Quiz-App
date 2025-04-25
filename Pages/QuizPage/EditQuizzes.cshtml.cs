using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuizPage
{
    public class EditQuizzesModel : PageModel
    {
        private readonly AppDbContext _context;
        public EditQuizzesModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz newQuiz { get; set; }

        public IEnumerable<Quiz> Quizzes { get; set; }= new List<Quiz>(); // Initialize the list to avoid null reference exceptions

        public void OnGet(int id) // fetch the id that we set in the form
        {
            newQuiz = _context.Quizzes.Find(id); // Fetch all quizzes from the database
        }
        public async Task<IActionResult> OnPost()

        {
            _context.Quizzes.Update(newQuiz); // Update the quiz in the database

            await _context.SaveChangesAsync();

            return RedirectToPage("Quizzes");

        }

    }
}
