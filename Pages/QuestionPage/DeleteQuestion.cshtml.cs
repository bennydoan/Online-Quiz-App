using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuestionPage
{
    public class DeleteQuestionModel : PageModel
    {
        private readonly AppDbContext _context;


        public DeleteQuestionModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question newQuestion { get; set; }

        public IEnumerable<Question> Questions { get; set; } = new List<Question>(); // Initialize the list to avoid null reference exceptions

        public void OnGet(int id )
        {
            newQuestion = _context.Questions.Find(id); // Fetch all questions from the database
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var questionToDelete = await _context.Questions.FindAsync(id); // Find the question object in the database
            

            int quizId = questionToDelete.QuizId; // Get the QuizId before deleting the question because we need it for redirection

            // Remove the question from the database
            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Redirect back to the QuestionCreate page with the QuizId
            return RedirectToPage("QuestionCreate", new { id = quizId });
        }
    }
}
