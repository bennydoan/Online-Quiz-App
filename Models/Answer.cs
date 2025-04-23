using System.ComponentModel.DataAnnotations;

namespace Quiz_Application2.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
