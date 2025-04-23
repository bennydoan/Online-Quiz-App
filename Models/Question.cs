using System.ComponentModel.DataAnnotations;

namespace Quiz_Application2.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public List<Answer> Answers { get; set; }

        public int CorrectAnswerId { get; set; }

    }
}
