using System.ComponentModel.DataAnnotations;

namespace Quiz_Application2.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Description { get; set; }

        public List<Question> Questions { get; set; }

    }
}
