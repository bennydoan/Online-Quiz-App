using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz_Application2.Data;
using Quiz_Application2.Models;
using System.Threading.Tasks;

namespace Quiz_Application2.Pages.QuestionPage
{
    public class QuestionCreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public string QuizTitle { get; set; } // Property to store the quiz title


        public QuestionCreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Answer> newAnswers { get; set; } = new List<Answer>
         {
            new Answer(),
            new Answer(),
            new Answer(),
            new Answer()
         };

        [BindProperty]
        public Question newQuestion { get; set; }

        public IEnumerable<Answer> Answers { get; set; } = new List<Answer>(); // Initialize the list to avoid null reference exceptions

        public IEnumerable<Question> Questions { get; set; } = new List<Question>(); // Initialize the list to avoid null reference exceptions

        public IEnumerable<SelectListItem> QuizList { get; set; }

        public void OnGet(int id) // id here is the quizid
        {
            // Fetch the quiz title
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == id); //Linq
            if (quiz != null)
            {
                QuizTitle = quiz.Title; // Store the title for use in the Razor page
            }

            // Fetch all questions for the quiz
            Questions = _context.Questions.Where(q => q.QuizId == id).ToList();

            // Fetch all answers for the questions in the quiz
            var questionIds = Questions.Select(q => q.Id).ToList(); //  extracts the IDs of all the retrieved questions into a list (questionIds).
            Answers = _context.Answers.Where(a => questionIds.Contains(a.QuestionId)).ToList(); // find all answers where the QuestionId matches any of the IDs in questionIds.
        }


        public async Task<IActionResult> OnPost(int id)
        {
            // Set the QuizId for the new question
            newQuestion.QuizId = id;

            // Save the question to generate its Id
            await _context.Questions.AddAsync(newQuestion);
            await _context.SaveChangesAsync(); // Save changes to generate the QuestionId

            // Set the QuestionId for each answer and save them
            foreach (var answer in newAnswers)
            {
                answer.QuestionId = newQuestion.Id;
            }
            await _context.Answers.AddRangeAsync(newAnswers);
            await _context.SaveChangesAsync(); // Save the answers

            // Redirect to the OnGet method to refresh the data
            return RedirectToPage("QuestionCreate", new { id });
        }

    }
}