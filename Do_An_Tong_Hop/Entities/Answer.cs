namespace API.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public string? StudentId { get; set; }
        public string AnswerContent { get; set; }
        public Question Question { get; set; } // Navigation property

    }
}
