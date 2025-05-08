using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz_Application2.Data;
using Quiz_Application2.Models;
using Microsoft.EntityFrameworkCore; // Add this namespace for Include()

using System.Collections.Generic;
using System.Linq;

namespace Quiz_Application2.Pages.QuizPage
{
    public class ScorePageModel : PageModel
    {
        private readonly AppDbContext _context;

        public ScorePageModel(AppDbContext context)
        {
            _context = context;
        }

        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

        public void OnGet()
        {
            // Fetch all quiz results, including the related Quiz data
            QuizResults = _context.QuizResults
                .Include(qr => qr.Quiz) // Ensure the Quiz navigation property is loaded
                .OrderByDescending(q => q.TakenAt)
                .ToList();
        }
    }
}