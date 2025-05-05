using System.ComponentModel.DataAnnotations;

namespace Quiz_Application2.Models
{
    public class Question
    {
    public int Id { get; set; }

    [Required(ErrorMessage = "This field cannot be empty")]
    public string Text { get; set; }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }

        //A question can have multiple answers
     public List<Answer> Answers { get; set; } = new List<Answer>();

    }
}
