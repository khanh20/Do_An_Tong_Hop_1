using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Answer
{
    public class CreateAnswerDto
    {

        public int QuestionId { get; set; }
        public string? StudentId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trường này không được bỏ trống")]
        [MaxLength(300, ErrorMessage = "Trường này tối đa 300 kí tự")]
        public string AnswerContent { get; set; }

    }
}
