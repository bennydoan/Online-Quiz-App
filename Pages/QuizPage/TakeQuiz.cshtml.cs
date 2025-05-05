using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;

namespace Quiz_Application2.Pages.QuizPage
{
    public class TakeQuizModel : PageModel
    {
        private readonly AppDbContext _context;

        public string QuizTitle { get; set; } // Property to store the quiz title
        public int CorrectAnswerCount { get; set; } // Stores the count of correct answers

        public Dictionary<int, bool> AnswerResults { get; set; } = new Dictionary<int, bool>(); // Stores if the selected answer is correct (QuestionId -> IsCorrect)

        public TakeQuizModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public IEnumerable<Answer> Answers { get; set; } = new List<Answer>();

        public void OnGet(int id)
        {
            // Fetch all questions for the quiz
            Questions = _context.Questions.Where(q => q.QuizId == id).ToList();

            // Fetch all answers for the questions in the quiz
            var questionIds = Questions.Select(q => q.Id).ToList();
            Answers = _context.Answers.Where(a => questionIds.Contains(a.QuestionId)).ToList();

            // Fetch the quiz title
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == id);
            if (quiz != null)
            {
                QuizTitle = quiz.Title;
            }
        }

        public IActionResult OnPost(int id)
        {
            // Fetch all questions for the quiz
            Questions = _context.Questions.Where(q => q.QuizId == id).ToList();

            // Fetch all answers for the questions in the quiz
            var questionIds = Questions.Select(q => q.Id).ToList();
            Answers = _context.Answers.Where(a => questionIds.Contains(a.QuestionId)).ToList();

            // Process selected answers
            CorrectAnswerCount = 0; // Initialize the correct answer count

            foreach (var question in Questions)
            {
                var selectedAnswerId = Request.Form[$"Question_{question.Id}"]; // Get the selected answer ID from the form
                if (!string.IsNullOrEmpty(selectedAnswerId))
                {
                    int answerId = int.Parse(selectedAnswerId);
                    var answer = Answers.FirstOrDefault(a => a.Id == answerId);
                    if (answer != null)
                    {
                        // Compare the selected answer with the correct answer
                        AnswerResults[question.Id] = answer.IsCorrect;
                        if(answer.IsCorrect)
                        {
                            CorrectAnswerCount++; // Increment the correct answer count
                        }
                    }
                }
            }

            // Return to the same page with the results
            return Page();
        }
    }
}
