using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public Answer newAnswer { get; set; } // Property to bind the new answer object


        [BindProperty]
        public Question newQuestion { get; set; }

        public IEnumerable<Answer> Answers { get; set; } = new List<Answer>(); // Initialize the list to avoid null reference exceptions

        public IEnumerable<Question> Questions { get; set; } = new List<Question>(); // Initialize the list to avoid null reference exceptions

        public IEnumerable<SelectListItem> QuizList { get; set; }

        public void OnGet(int id)
        {
            //Fetch the quiz title from the Quiz model

            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == id);// FirstorDefault returns the first element of a sequence or a default value if no such element is found
            if (quiz != null)
            {
                QuizTitle = quiz.Title; // Store the title for use in the Razor page
            }

            newQuestion = new Question { QuizId = id }; // Pre-fill QuizId == route for the new question
            Questions = _context.Questions.Where(q => q.QuizId == id).ToList(); // Fetch questions for the specific quiz

        }

        public async Task<IActionResult> OnPost(int id)
        {
            newQuestion.QuizId = id;

            await _context.Questions.AddAsync(newQuestion);
           
            await _context.SaveChangesAsync();
            return RedirectToPage("QuestionCreate",new {id});

        }


    }
}


