using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quiz_Application2.Data;
using Quiz_Application2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quiz_Application2.Pages.QuizPage
{
    public class QuizResultsModel : PageModel
    {
        private readonly AppDbContext _context;

        public QuizResultsModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

        public void OnGet()
        {
            // Fetch all quiz results, including the related quiz details
            QuizResults = _context.QuizResults
                .Include(qr => qr.Quiz)
                .OrderByDescending(qr => qr.TakenAt)
                .ToList();
        }
    }
}
