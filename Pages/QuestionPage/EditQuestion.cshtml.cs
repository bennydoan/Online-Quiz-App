using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quiz_Application2.Data;
using Quiz_Application2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_Application2.Pages.QuestionPage
{
    public class EditQuestionModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditQuestionModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public List<Answer> Answers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (Question == null)
            {
                return NotFound();
            }

            Answers = Question.Answers.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Load the existing question and its answers
            var questionToUpdate = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == Question.Id);

            if (questionToUpdate == null)
            {
                return NotFound();
            }

            // Update question text
            questionToUpdate.Text = Question.Text;

            // Process each answer from the form
            foreach (var submittedAnswer in Answers)
            {
                if (submittedAnswer.Id != 0)
                {
                    // Update existing answer
                    var existingAnswer = await _context.Answers.FindAsync(submittedAnswer.Id);
                    if (existingAnswer != null)
                    {
                        existingAnswer.Text = submittedAnswer.Text;
                        existingAnswer.IsCorrect = submittedAnswer.IsCorrect;
                        _context.Entry(existingAnswer).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Add new answer
                    submittedAnswer.QuestionId = questionToUpdate.Id;
                    _context.Answers.Add(submittedAnswer);
                }
            }

            // Remove answers that weren't included in the form
            var answersToRemove = questionToUpdate.Answers
                .Where(a => !Answers.Any(submittedAnswer => submittedAnswer.Id == a.Id))
                .ToList();

            foreach (var answerToRemove in answersToRemove)
            {
                _context.Answers.Remove(answerToRemove);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["Message"] = "Question and answers updated successfully.";
                return RedirectToPage("/QuestionPage/QuestionCreate", new { id = questionToUpdate.QuizId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(q => q.Id == id);
        }
    }
}
