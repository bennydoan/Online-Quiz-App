using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuizPage
{
    public class DeleteQuizzesModel : PageModel
    {
        private readonly AppDbContext _context;
        public DeleteQuizzesModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz newQuiz { get; set; }

        public IEnumerable<Quiz> Quizzes { get; set; }= new List<Quiz>(); // Initialize the list to avoid null reference exceptions

        public void OnGet(int id)
        {
            newQuiz = _context.Quizzes.Find(id); // Fetch all quizzes from the database, and use ToList() to convert the result to a list
        }
        public async Task<IActionResult> OnPost(int id)

        {
            var quizToDelete = await _context.Quizzes.FindAsync(id); // fin the quiz object in the database



            // Remove the quiz from the database
            _context.Quizzes.Remove(quizToDelete);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect back to the quizzes list page
            return RedirectToPage("Quizzes");




        }

    }
}
