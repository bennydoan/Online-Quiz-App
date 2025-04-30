using System.ComponentModel.DataAnnotations;

namespace Quiz_Application2.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Answer text is required")]
        public string Text { get; set; } // Each answer has a single text value

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool IsCorrect { get; set; } // Indicates if this answer is correct


    }
}
