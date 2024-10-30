using API.Entities;

namespace API.Dtos.Answer
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public string? StudentId { get; set; }
        public string AnswerContent { get; set; }


    }
}
