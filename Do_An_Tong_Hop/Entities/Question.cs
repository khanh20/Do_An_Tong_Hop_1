namespace API.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }

        public ICollection<Answer>? Answers { get; set; }

    }
}
