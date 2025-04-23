namespace Quiz_Application2.Models
{
    public class QuizResult
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public DateTime TakenAt { get; set; } = DateTime.Now;

        // Link to the Quiz
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

    }
}
